using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2.Controllers
{
    public class GraphController : Controller
    {
        public ActionResult UserPerformanceGraph(string userName)
        {
            UserPerformanceGraphData baseData = new UserPerformanceGraphData();

            int userId = Utility.GetUserId(userName);

            using (var db = new ProTeamManagerContext())
            {
                string status = TaskStatus.Completed.ToString();
                List<Task> completedTasks = db.Tasks.Where(t => t.taskStatus == status && t.AssignedToUserID == userId).Select(t => t).OrderBy(t => t.endUtc).ToList();
                List<int> taskCounts = new List<int>();
                List<int> ratings = new List<int>();

                for (int i = 1; i <= completedTasks.Count; i++)
                {
                    taskCounts.Add(i);
                }

                foreach (var task in completedTasks)
                {
                    ratings.Add(task.rating);
                }

                baseData.tasks = taskCounts.ToArray();
                baseData.ratings = ratings.ToArray();
            }

            return View(baseData);
        }


        public ActionResult UserStoryProgress(int id)
        {
            ProgressData baseData = new ProgressData();
            baseData.url = Url.Action("GenerateUserStoryProgressGraph", new { id = id });
            baseData.scale = "X-Axis : Tasks Y-Axis : Progress";
            return View("Progress", baseData);
        }

        public ActionResult GenerateUserStoryProgressGraph(int id)
        {
            ProgressGraphData graphData = new ProgressGraphData();
            string status = TaskStatus.Completed.ToString();
            using (var db = new ProTeamManagerContext())
            {
                var tasks = db.Tasks.Where(t => t.fk_userStoryId == id).Select(t => t).OrderBy(t=>t.endUtc).ToList();
                List<int> xValList = new List<int>();
                List<int> yValList = new List<int>();

                foreach (var t in tasks)
                {
                    if (t.taskStatus == status)
                    {
                        yValList.Add(5);
                    }
                    else
                    {
                        yValList.Add(0);
                    }
                }

                for (int i = 1; i <= yValList.Count; i++)
                {
                    xValList.Add(i);
                }
                graphData.xValues = xValList.ToArray();
                graphData.yValues = yValList.OrderByDescending(t=>t).ToArray();

            }
            return View("ProgressGraph", graphData);
        }

        public ActionResult SprintProgress(int id)
        {
            ProgressData baseData = new ProgressData();
            baseData.url = Url.Action("GenerateSprintProgressGraph", new { id = id });
            baseData.scale = "X-Axis : UserStories Y-Axis : Progress";
            return View("Progress", baseData);
        }

        public ActionResult GenerateSprintProgressGraph(int id)
        {
            ProgressGraphData graphData = new ProgressGraphData();
            string status = TaskStatus.Completed.ToString();
            using (var db = new ProTeamManagerContext())
            {
                var userStories = db.UserStories.Where(t => t.fk_SprintId == id).Select(t => t).ToList();
                
                List<int> xValList = new List<int>();
                List<int> yValList = new List<int>();

                foreach (var us in userStories)
                {
                    if (db.Tasks.Where(t => t.fk_userStoryId == us.Id && t.taskStatus != status).Select(t => t).Any())
                    {
                        yValList.Add(0);
                    }
                    else
                    {
                        yValList.Add(5);
                    }
                }

                for (int i = 1; i <= yValList.Count; i++)
                {
                    xValList.Add(i);
                }
                graphData.xValues = xValList.ToArray();
                graphData.yValues = yValList.OrderByDescending(t => t).ToArray();

            }
            return View("ProgressGraph", graphData);
        }

    }
}
