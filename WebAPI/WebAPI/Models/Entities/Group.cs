using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models.Entities
{
    public partial class Group
    {
        public Group()
        {
            Memberships = new HashSet<Membership>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? NumberMember { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
