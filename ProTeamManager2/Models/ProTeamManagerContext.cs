using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ProTeamManager2.Models
{
    public class ProTeamManagerContext : DbContext
    {
        public ProTeamManagerContext()
            : base("name = DefaultConnection")
        {

        }

        public DbSet<TeamModel> Teams { get; set; }
        public DbSet<TeamMembers> TeamMembers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<UserStory> UserStories { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}