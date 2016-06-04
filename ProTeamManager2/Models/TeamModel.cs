using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrganizationName { get; set; }
        public string DivisionName { get; set; }
        public int OwnedBy { get; set; }
        public int NoOfMembers { get; set; }
    }
}
