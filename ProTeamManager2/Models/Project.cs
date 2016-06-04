using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required]
        public string projectName { get; set; }
        public string projectDescription { get; set; }
        public int fk_teamId { get; set; }
        public int fk_managerId { get; set; }
        [NotMapped]
        public string ownedBy { get; set; }
        [NotMapped]
        public string associatedTeam { get; set; }

    }
}