using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;
using WebMatrix.WebData;
using System.Web.Security;
using ProTeamManager2.Filters;

namespace ProTeamManager2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            bool isManager = User.IsInRole("Manager");
            TempData["isManager"] = isManager;
            if (isManager)
            {
                HomeDataModel baseData = new HomeDataModel();
                baseData.teams = GetTeams();
                baseData.allUsers = Utility.GetAllUsers();
                baseData.projects = GetProjects();

                return View(baseData);
            }
            else
            {
                UserDashboardData data = new UserDashboardData();
                data.isManager = isManager;
                data.allTasks = GetTasksForUser(GetCurrentUserId());
                return View("UserDashboard", data);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult GetTasksForTeam(int Id)
        {
            UserDashboardData data = new UserDashboardData();
            data.teamId = Id;
            if (TempData["isManager"] != null)
            {
                data.isManager = (bool)TempData["isManager"];
            }
            using (var db = new ProTeamManagerContext())
            {
                
                data.allTasks = (from team in db.Teams
                              join member in db.TeamMembers on team.Id equals member.TeamId
                              join task in db.Tasks on member.UserId equals task.AssignedToUserID
                               where team.Id == Id
                              select task).ToList();

                data.allUsers = (from team in db.Teams
                                 join member in db.TeamMembers on team.Id equals member.TeamId
                                 join user in db.Users on member.UserId equals user.Id
                                 where team.Id == Id
                                 select user).ToList();

                data.allComments = new List<Feedback>();

                foreach (var task in data.allTasks)
                {
                    List<Comment> comments = db.Comments.Where(t => t.fk_taskId == task.Id).Select(t => t).ToList();

                    foreach (var comment in comments)
                    {
                        Feedback feedback = new Feedback();
                        feedback.taskName = task.taskName;
                        feedback.Comment = comment.comment;
                        feedback.CommentBy = comment.commentBy;

                        data.allComments.Add(feedback);
                    }
                }
                 
            }

            return View("UserDashboard", data);
        }
       

        [HttpGet]
        public ActionResult CreateNewTeam()
        {
            var team = new TeamModel();
            return View(team);
        }

        [HttpPost]
        public ActionResult CreateNewTeam(TeamModel newTeam)
        {
            using (var db = new ProTeamManagerContext())
            {
                newTeam.OwnedBy = GetCurrentUserId();
                db.Teams.Add(newTeam);
                db.SaveChanges();
            }
            return Redirect("/");
        }

        [HttpGet]
        public ActionResult EditTeam(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var team = db.Teams
                    .SingleOrDefault(t => t.Id == id);

                if (team == null)
                {
                    return HttpNotFound();
                }

                return View(team);
            }
        }

        [HttpPost]
        public ActionResult EditTeam(int id, TeamModel team)
        {
            using (var db = new ProTeamManagerContext())
            {

                var teamToBeEdited = db.Teams
                    .SingleOrDefault(p => p.Id == id);

                teamToBeEdited.Name = team.Name;
                teamToBeEdited.OrganizationName = team.OrganizationName;
                teamToBeEdited.DivisionName = team.DivisionName;
                teamToBeEdited.OwnedBy = team.OwnedBy;

                db.SaveChanges();

                return Redirect("/");
            }
        }

        public ActionResult DeleteTeam(int id)
        {
            using (var db = new ProTeamManagerContext())
            {
                var teamToBeRemoved = db.Teams.Where(t => t.Id == id).SingleOrDefault();
                db.Teams.Remove(teamToBeRemoved);
                db.SaveChanges();
            }
            return Redirect("/");
        }

        # region Helper Methods

        public int GetCurrentUserId()
        {
            string currentUserName = User.Identity.Name;
            using (var db = new ProTeamManagerContext())
            {
                return db.Users.Where(t => t.UserName == currentUserName).Select(t => t.Id).FirstOrDefault();

            }

        }

        public List<TeamModel> GetTeams()
        {
            List<TeamModel> result = new List<TeamModel>();
            int currentUserID = GetCurrentUserId();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Teams.Where(t => t.OwnedBy == currentUserID).Select(t => t).ToList();

                foreach (var team in result)
                {
                    team.NoOfMembers = db.TeamMembers.Where(t => t.TeamId == team.Id).Select(t => t).Count();
                }
            }

            return result;
        }

        public List<Project> GetProjects()
        {
            List<Project> result = new List<Project>();
            int currentUserID = GetCurrentUserId();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Projects.Where(t => t.fk_managerId == currentUserID).Select(t => t).ToList();

                foreach (var project in result)
                {
                    string ownerName = Utility.GetUserName(project.fk_managerId);
                    string teamName = Utility.GetTeamName(project.fk_teamId);
                    project.ownedBy = String.IsNullOrEmpty(ownerName)? "-" : ownerName;
                    project.associatedTeam = String.IsNullOrEmpty(teamName) ? "-" : teamName;
                }
            }

            return result;
        }

        public List<Task> GetTasksForUser(int userId)
        {
            List<Task> result = new List<Task>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Tasks.Where(t => t.AssignedToUserID == userId).Select(t => t).ToList();
            }

            return result;
        }

       

        public int AddMemberToTeam(int teamId, string userName)
        {
            int result = 0;
            int userId = Utility.GetUserId(userName);

            using (var db = new ProTeamManagerContext())
            {
                var tmOld = db.TeamMembers.Where(t => t.TeamId == teamId && t.UserId == userId).Select(t => t).FirstOrDefault();

                if (tmOld == null)
                {
                    TeamMembers tm = new TeamMembers();
                    tm.TeamId = teamId;
                    tm.UserId = userId;
                    db.TeamMembers.Add(tm);
                    result = db.SaveChanges();
                }
                else
                {
                    return 1;
                }
            }

            if (result > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public int AddTeam(int projectId, string teamName)
        {
            int result = 0;
            int teamId = Utility.GetTeamID(teamName);

            using (var db = new ProTeamManagerContext())
            {
                var project = db.Projects.Where(t => t.Id == projectId ).Select(t => t).FirstOrDefault();

                if (project == null)
                {

                    return -1;
                }
                else
                {
                    project.fk_teamId = teamId;
                    db.SaveChanges();
                    result = 1;
                }
            }

            if (result > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public int ChangeTaskStatus(int taskId, string status)
        {
            int result = -1;

            using (var db = new ProTeamManagerContext())
            {
                var taskToBeUpdated = db.Tasks.Where(t => t.Id == taskId).Select(t => t).FirstOrDefault();

                if (taskToBeUpdated != null && taskToBeUpdated.taskStatus != status)
                {
                    if (status == TaskStatus.InProgress.ToString())
                        taskToBeUpdated.startUtc = DateTime.UtcNow;
                    if (status == TaskStatus.Completed.ToString())
                    {
                        taskToBeUpdated.endUtc = DateTime.UtcNow;
                        taskToBeUpdated.actualHours = (int)(taskToBeUpdated.endUtc - taskToBeUpdated.startUtc).TotalHours;
                    }
                    taskToBeUpdated.taskStatus = status;
                    db.SaveChanges();
                    result = 1;
                }
            }



            return result;
        }

        public int ChangeTaskPriority(int taskId)
        {
            int result = -1;
            using (var db = new ProTeamManagerContext())
            {
                var taskToBeUpdated = db.Tasks.Where(t => t.Id == taskId).Select(t => t).FirstOrDefault();

                if (taskToBeUpdated != null)
                {
                    taskToBeUpdated.isHighPriority = taskToBeUpdated.isHighPriority ? false : true;
                    db.SaveChanges();
                    result = 1;
                }
            }

            return result;

        }

        #endregion


        public string Rate(int taskId)
        {
            return Url.Action("ReturnRate", new { taskId = taskId });
        }

        public ActionResult ReturnRate(int taskId)
        {
            ViewBag.taskID = taskId;
            return View("Rate");
        }

        public string AddFeedback(int taskId, int rating, string comment)
        {
            using (var db = new ProTeamManagerContext())
            {
                var taskToBeUpdated = db.Tasks.Where(t => t.Id == taskId).Select(t => t).FirstOrDefault();

                if (taskToBeUpdated != null)
                {
                    taskToBeUpdated.rating = rating;
                    db.SaveChanges();
                }

                var commentToBeUpdated = db.Comments.Where(t => t.fk_taskId == taskId && t.commentBy == User.Identity.Name).Select(t => t).FirstOrDefault();
                if (commentToBeUpdated == null)
                {
                    Comment commentToBeSaved = new Comment();
                    commentToBeSaved.fk_taskId = taskId;
                    commentToBeSaved.comment = comment;
                    commentToBeSaved.commentBy = User.Identity.Name;

                    db.Comments.Add(commentToBeSaved);
                    db.SaveChanges();
                }
                else
                {
                    commentToBeUpdated.comment = comment;
                    db.SaveChanges();
                }
            }
            return Url.Action("Index");
        }
    }
}
