using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProTeamManager2.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {

        public ActionResult Team()
        {
            return View();
        }

    }
}
