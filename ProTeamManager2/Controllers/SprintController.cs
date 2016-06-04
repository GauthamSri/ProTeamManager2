using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    public class SprintController : Controller
    {

        public ActionResult Sprint(int id)
        {
            SprintData baseData = new SprintData();
            baseData.allSprints = GetSprints(id);
            TempData["releaseID"] = id;
            return View(baseData);
        }

        public List<Sprint> GetSprints(int releaseId)
        {
            List<Sprint> result = new List<Sprint>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Sprints.Where(t => t.fk_releaseId == releaseId).Select(t => t).ToList();

                foreach (var sprint in result)
                {
                    sprint.associatedRelease = "Release" + Utility.GetReleaseNumber(sprint.fk_releaseId);
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
        public ActionResult Create(Sprint sprint)
        {
            int id = (int)TempData["releaseID"];
            using (var db = new ProTeamManagerContext())
            {               
                sprint.fk_releaseId = id;
                db.Sprints.Add(sprint);
                db.SaveChanges();
            }

            var previousUrl = this.Url.Action("Sprint", "Sprint", new { id = id }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        public ActionResult Delete(int id)
        {
            int releaseId = (int)TempData["releaseID"];
            using (var db = new ProTeamManagerContext())
            {
                var sprintToBeRemoved = db.Sprints.Where(t => t.Id == id).SingleOrDefault();
                db.Sprints.Remove(sprintToBeRemoved);
                db.SaveChanges();
            }
            var previousUrl = this.Url.Action("Sprint", "Sprint", new { id = releaseId }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var sprint = db.Sprints
                    .SingleOrDefault(t => t.Id == id);

                if (sprint == null)
                {
                    return HttpNotFound();
                }

                return View(sprint);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Sprint sprint)
        {
            using (var db = new ProTeamManagerContext())
            {

                var sprintToBeEdited = db.Sprints.SingleOrDefault(p => p.Id == id);

                sprintToBeEdited.sprintNumber = sprint.sprintNumber;
                db.SaveChanges();

                var previousUrl = this.Url.Action("Release", "Release", new { id = sprintToBeEdited.fk_releaseId }, this.Request.Url.Scheme);

                return Redirect(previousUrl);

            }
        }

    }
}
