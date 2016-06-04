using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProTeamManager2.Models
{
    public class Release
    {
        public int Id { get; set; }
        public int NoOfSprints { get; set; }
        [Required]
        public int releaseNumber { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime releaseDate { get; set; }
        public int fk_ProjectId { get; set; }
        [NotMapped]
        public string projectName { get; set; }
    }
}