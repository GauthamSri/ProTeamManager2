using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProTeamManager2.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int fk_userStoryId { get; set; }
        public string taskName { get; set; }
        public string taskDesccription { get; set; }
        public string taskStatus { get; set; }
        public int AssignedToUserID { get; set; }
        public int estimatedHours { get; set; }
        public bool isHighPriority { get; set; }
        public int actualHours { get; set; }
        [DataType(DataType.Date)]
        public DateTime startUtc { get; set; }
        [DataType(DataType.Date)]
        public DateTime endUtc { get; set; }
        public int rating { get; set; }
        [NotMapped]
        public string AssignedTo { get; set; }
    }
}