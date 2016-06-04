using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTeamManager2.Models
{
    public class UserDashboardData
    {
        public List<Task> allTasks;
        public List<Feedback> allComments;
        public List<User> allUsers;
        public int teamId { get; set; }
        public bool isManager { get; set; }
    }
}