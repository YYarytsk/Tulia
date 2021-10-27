using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class UserWithGroup
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? NumberGroups { get; set; }

        public List<Group> Groups { get; set; }
        public List<Membership> Memberships { get; set; }
    }
}
