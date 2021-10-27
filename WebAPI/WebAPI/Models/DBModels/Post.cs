using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedTime { get; set; }
        public int GroupId { get; set; }

        public Post() { }

        public Post(int Id, int UserId, string Title, string Body, DateTime CreatedTime, int GroupId)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Title = Title;
            this.Body = Body;
            this.CreatedTime = CreatedTime;
            this.GroupId = GroupId;
        }
        public Post(int Id, int UserId, string Title, string Body, DateTime CreatedTime)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Title = Title;
            this.Body = Body;
            this.CreatedTime = CreatedTime;
        }
    }
}
