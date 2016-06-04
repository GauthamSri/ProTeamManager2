using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    public class ReleaseController : Controller
    {
        
        public ActionResult Release(int id)
        {
            ReleaseData baseData = new ReleaseData();
            baseData.allReleases = GetReleases(id);
            TempData["projectID"] = id;

            return View(baseData);
        }

        public List<Release> GetReleases(int projectId)
        {
            List<Release> result = new List<Release>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Releases.Where(t => t.fk_ProjectId == projectId).Select(t => t).ToList();

                foreach (var release in result)
                {
                    string projectName = Utility.GetProjectName(release.fk_ProjectId);
                    release.projectName = String.IsNullOrEmpty(projectName) ? "-" : projectName;
                    release.NoOfSprints = db.Sprints.Where(t => t.fk_releaseId == release.Id).Select(t => t).Count();
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
        public ActionResult Create(Release release)
        {
            int id = (int)TempData["projectID"];
            using (var db = new ProTeamManagerContext())
            {
                release.fk_ProjectId = id;
                db.Releases.Add(release);
                db.SaveChanges();
            }

            var previousUrl = this.Url.Action("Release", "Release", new { id = id }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        public ActionResult Delete(int id)
        {
            int projectId = (int)TempData["projectID"];
            using (var db = new ProTeamManagerContext())
            {
                var releaseToBeRemoved = db.Releases.Where(t => t.Id == id).SingleOrDefault();
                db.Releases.Remove(releaseToBeRemoved);
                db.SaveChanges();
            }
            var previousUrl = this.Url.Action("Release", "Release", new { id = projectId }, this.Request.Url.Scheme);

            return Redirect(previousUrl);
        }

        [HttpGet]
        public ActionResult Edit(int id = -1)
        {
            using (var db = new ProTeamManagerContext())
            {
                var release = db.Releases
                    .SingleOrDefault(t => t.Id == id);

                if (release == null)
                {
                    return HttpNotFound();
                }

                return View(release);
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, Release release)
        {
            using (var db = new ProTeamManagerContext())
            {

                var releaseToBeEdited = db.Releases.SingleOrDefault(p => p.Id == id);

                releaseToBeEdited.releaseNumber = release.releaseNumber;
                releaseToBeEdited.NoOfSprints = release.NoOfSprints;
                db.SaveChanges();

                var previousUrl = this.Url.Action("Release", "Release", new { id = releaseToBeEdited.fk_ProjectId }, this.Request.Url.Scheme);

                return Redirect(previousUrl);

            }
        }

        public int SetReleaseDate(int Id, string currentDate)
        {
            int result = 0;

            if (!String.IsNullOrEmpty(currentDate))
            {

                using (var db = new ProTeamManagerContext())
                {
                    var releaseToBeUpdated = db.Releases.Where(t => t.Id == Id).Select(t => t).FirstOrDefault();

                    if (releaseToBeUpdated != null)
                    {
                        string newDate = currentDate.Substring(0, currentDate.IndexOf('-'));

                        DateTime dt = Convert.ToDateTime(newDate);

                        releaseToBeUpdated.releaseDate = dt.AddDays(1); ;
                        db.SaveChanges();
                        result = 1;
                    }
                }
            }

            return result;
        }


    }
}
