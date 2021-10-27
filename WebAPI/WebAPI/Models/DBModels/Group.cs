using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class Group
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? NumberMember { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }

        public Group() { }

        public Group(int Id, int? NumberMember)
        {
            this.Id = Id;
            this.NumberMember = NumberMember;
        }
        public Group(int userId, string GroupTitle, string Description)
        {
            this.UserId = UserId;
            this.GroupTitle = GroupTitle;
            this.Description = Description;
        }

        public Group(int Id, int UserId, int? NumberMember, string GroupTitle, string Description)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.NumberMember = NumberMember;
            this.GroupTitle = GroupTitle;
            this.Description = Description;
        }
    }
}
