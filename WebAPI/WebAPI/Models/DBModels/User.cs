using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? NumberGroups { get; set; }

        public User() { }

        public User(int Id, int? NumberGroups)
        {
            this.Id = Id;
            this.NumberGroups = NumberGroups;
        }
        public User(int Id, string FirstName, string LastName, string Username)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
        }

        public User(string Username, string Password, string FirstName, string LastName)
        {
            this.Username = Username;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public User(int Id, string FirstName, string LastName, string Username, string Role, int? NumberGroups)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
            this.Role = Role;
            this.NumberGroups = NumberGroups;
        }

        public User(int Id, string FirstName, string LastName, string Username, string password, string Role, int? NumberGroups)
        {
            this.Id = Id;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Username = Username;
            this.Role = Role;
            this.Password = password;
            this.NumberGroups = NumberGroups;
        }
    }
}
