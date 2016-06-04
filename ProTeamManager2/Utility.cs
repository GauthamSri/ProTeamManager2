using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProTeamManager2.Models;

namespace ProTeamManager2
{
    public class Utility
    {

        public static List<User> GetAllUsers()
        {
            List<User> result = new List<User>();

            using (var db = new ProTeamManagerContext())
            {
                result = db.Users.Select(t => t).ToList();

            }
            return result;
        }

       
        public static int GetUserId(string UserName)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Users.Where(t => t.UserName == UserName).Select(t => t.Id).FirstOrDefault();

            }

        }

        public static string GetUserName(int userId)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Users.Where(t => t.Id == userId).Select(t => t.UserName).FirstOrDefault();

            }

        }

        public static string GetTeamName(int Id)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Teams.Where(t => t.Id == Id).Select(t => t.Name).FirstOrDefault();

            }

        }

        public static string GetProjectName(int Id)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Projects.Where(t => t.Id == Id).Select(t => t.projectName).FirstOrDefault();

            }

        }

        public static int GetTeamID(string teamName)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Teams.Where(t => t.Name == teamName).Select(t => t.Id).FirstOrDefault();

            }

        }

        public static int GetReleaseNumber(int Id)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Releases.Where(t => t.Id == Id).Select(t => t.releaseNumber).FirstOrDefault();

            }

        }
        public static int GetSprintNumber(int Id)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Sprints.Where(t => t.Id == Id).Select(t => t.sprintNumber).FirstOrDefault();

            }

        }

        public static List<TeamModel> GetAllTeams(int Id)
        {
            using (var db = new ProTeamManagerContext())
            {
                return db.Teams.Where(t => t.OwnedBy == Id).Select(t => t).ToList();
            }
        }
    }
}