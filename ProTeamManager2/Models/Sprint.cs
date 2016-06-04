using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProTeamManager2.Models
{
    public class Sprint
    {
        public int Id { get; set; }
        public int sprintNumber { get; set; }
        public int fk_releaseId { get; set; }
        [NotMapped]
        public string associatedRelease { get; set; }
    }
}