using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        [HttpGet]
        public ActionResult Create()
        {
            var project = new Project();
            return View(project);
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            using (var db = new ProTeamManagerContext())
            {
                project.fk_managerId = GetCurrentUserId();
                db.Projects.Add(project);
                db.SaveChanges();
            }
            return Redirect("/");
        }


        [HttpGet]
        public ActionResult EditProject(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var project = db.Projects
                    .SingleOrDefault(t => t.Id == id);

                if (project == null)
                {
                    return HttpNotFound();
                }

                return View(project);
            }
        }

        [HttpPost]
        public ActionResult EditProject(int id, Project project)
        {
            using (var db = new ProTeamManagerContext())
            {

                var projectToBeEdited = db.Projects.SingleOrDefault(p => p.Id == id);

                projectToBeEdited.projectName = project.projectName;
                projectToBeEdited.projectDescription = project.projectDescription;
                projectToBeEdited.fk_teamId = project.fk_teamId;

                db.SaveChanges();

                return Redirect("/");
            }
        }


        public int GetCurrentUserId()
        {
            string currentUserName = User.Identity.Name;
            using (var db = new ProTeamManagerContext())
            {
                return db.Users.Where(t => t.UserName == currentUserName).Select(t => t.Id).FirstOrDefault();

            }

        }

        public ActionResult DeleteProject(int id)
        {
            using (var db = new ProTeamManagerContext())
            {
                var projectToBeRemoved = db.Projects.Where(t => t.Id == id).SingleOrDefault();
                db.Projects.Remove(projectToBeRemoved);
                db.SaveChanges();
            }
            return Redirect("/");
        }
    }
}
