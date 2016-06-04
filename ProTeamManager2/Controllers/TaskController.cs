using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    public class TaskController : Controller
    {

        public ActionResult Task(int id)
        {
            TaskData baseData = new TaskData();
            baseData.allTasks = GetAllTasks(id);
            baseData.allUsers = GetAllUsers();
            TempData["userStoryID"] = id;
            return View(baseData);
        }

        public List<Task> GetAllTasks(int userStoryId)
        {
            List<Task> result = new List<Task>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Tasks.Where(t => t.fk_userStoryId == userStoryId).Select(t => t).ToList();

                foreach (var task in result)
                {
                    task.AssignedTo = Utility.GetUserName(task.AssignedToUserID);
                }

            }

            return result;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Task task)
        {
            int id = (int)TempData["userStoryID"];
            using (var db = new ProTeamManagerContext())
            {
                task.fk_userStoryId = id;
                task.taskStatus = TaskStatus.Created.ToString();

                if (task.startUtc == DateTime.MinValue)
                {
                    task.startUtc = DateTime.MaxValue;
                }

                if (task.endUtc == DateTime.MinValue)
                {
                    task.endUtc = DateTime.MaxValue;
                }
                db.Tasks.Add(task);
                db.SaveChanges();
            }

            var previousUrl = this.Url.Action("Task", "Task", new { id = id }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        public ActionResult Delete(int id)
        {
            int usId = (int)TempData["userStoryID"];
            using (var db = new ProTeamManagerContext())
            {
                var taskToBeRemoved = db.Tasks.Where(t => t.Id == id).SingleOrDefault();
                db.Tasks.Remove(taskToBeRemoved);
                db.SaveChanges();
            }
            var previousUrl = this.Url.Action("Task", "Task", new { id = usId }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var task = db.Tasks
                    .SingleOrDefault(t => t.Id == id);

                if (task == null)
                {
                    return HttpNotFound();
                }

                return View(task);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Task task)
        {
            using (var db = new ProTeamManagerContext())
            {

                var taskToBeEdited = db.Tasks.SingleOrDefault(p => p.Id == id);

                taskToBeEdited.taskName = task.taskName;
                taskToBeEdited.taskDesccription = task.taskDesccription;
                taskToBeEdited.taskStatus = task.taskStatus;
                db.SaveChanges();

                var previousUrl = this.Url.Action("Task", "Task", new { id = taskToBeEdited.fk_userStoryId }, this.Request.Url.Scheme);

                return Redirect(previousUrl);

            }
        }

        public List<User> GetAllUsers()
        {
            List<User> result = new List<User>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Users.Select(t => t).ToList();

            }
            return result;
        }

        public int GetUserId(string UserName)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Users.Where(t => t.UserName == UserName).Select(t => t.Id).FirstOrDefault();

            }

        }

        public string AssignToUser(int Id, string userName)
        {
           
            int userId = GetUserId(userName);
            int usId = (int)TempData["userStoryID"];
            var previousUrl = this.Url.Action("Task", "Task", new { id = usId }, this.Request.Url.Scheme);

            using (var db = new ProTeamManagerContext())
            {
                var task = db.Tasks.Where(t => t.Id == Id).Select(t => t).SingleOrDefault();

                if (task == null)
                {
                    return previousUrl;
                }
                else
                {
                    task.AssignedToUserID = userId;
                    db.SaveChanges();
                }
            }
            return previousUrl;
        }

    }
}
