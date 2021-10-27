using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models.Entities
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Groups = new HashSet<Group>();
            Memberships = new HashSet<Membership>();
            Posts = new HashSet<Post>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int? NumberGroups { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
