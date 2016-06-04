using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    public class UserstoryController : Controller
    {
        public ActionResult Userstory(int id)
        {
            UserstoryData baseData = new UserstoryData();
            baseData.allUserStories = GetAllUserStories(id);
            TempData["sprintID"] = id;
            return View(baseData);
        }

        public List<UserStory> GetAllUserStories(int sprintId)
        {
            List<UserStory> result = new List<UserStory>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.UserStories.Where(t => t.fk_SprintId == sprintId).Select(t => t).ToList();

                foreach (var us in result)
                {
                    us.associatedSprint = "Sprint" + Utility.GetSprintNumber(us.fk_SprintId);
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
        public ActionResult Create(UserStory userStory)
        {
            int id = (int)TempData["sprintID"];
            using (var db = new ProTeamManagerContext())
            {
                userStory.fk_SprintId = id;
                db.UserStories.Add(userStory);
                db.SaveChanges();
            }

            var previousUrl = this.Url.Action("Userstory", "Userstory", new { id = id }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        public ActionResult Delete(int id)
        {
            int usId = (int)TempData["sprintID"];
            using (var db = new ProTeamManagerContext())
            {
                var userStoryToBeRemoved = db.UserStories.Where(t => t.Id == id).SingleOrDefault();
                db.UserStories.Remove(userStoryToBeRemoved);
                db.SaveChanges();
            }
            var previousUrl = this.Url.Action("Userstory", "Userstory", new { id = usId }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var userStory = db.UserStories
                    .SingleOrDefault(t => t.Id == id);

                if (userStory == null)
                {
                    return HttpNotFound();
                }

                return View(userStory);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, UserStory userStory)
        {
            using (var db = new ProTeamManagerContext())
            {

                var userStoryToBeEdited = db.UserStories.SingleOrDefault(p => p.Id == id);

                userStoryToBeEdited.Name = userStory.Name;
                userStoryToBeEdited.descriprion = userStory.descriprion;
                db.SaveChanges();

                var previousUrl = this.Url.Action("Release", "Release", new { id = userStoryToBeEdited.fk_SprintId }, this.Request.Url.Scheme);

                return Redirect(previousUrl);

            }
        }

    }
}
