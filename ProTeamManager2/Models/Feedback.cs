using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class Feedback
    {
        public string taskName { get; set; }
        public int rating { get; set; }
        public string Comment { get; set; }
        public string CommentBy { get; set; }

    }
}