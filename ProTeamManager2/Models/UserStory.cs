using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProTeamManager2.Models
{
    public class UserStory
    {
        public int Id { get; set; }
        public int fk_SprintId { get; set; }
        public string Name { get; set; }
        public string descriprion { get; set; }
        public int estimatedDays { get; set; }
        [NotMapped]
        public string associatedSprint { get; set; }
    }
}