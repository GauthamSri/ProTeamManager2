using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int fk_taskId { get; set; }
        public string comment { get; set; }
        public string commentBy { get; set; }
    }
}