using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class TeamMembers
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
}