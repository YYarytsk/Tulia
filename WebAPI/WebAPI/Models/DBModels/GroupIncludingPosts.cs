using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class GroupIncludingPosts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? NumberMember { get; set; }
        public string GroupTitle { get; set; }
        public string Description { get; set; }

        public List<Post> Posts { get; set; }


        public GroupIncludingPosts() { }

        //public GroupIncludingPosts(int Id, int? NumberMember)
        //{
        //    this.Id = Id;
        //    this.NumberMember = NumberMember;
        //}
    }

}