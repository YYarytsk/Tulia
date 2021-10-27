using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class PostIncludingComments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedTime { get; set; }

   
        public List<Comment> Comments { get; set; }
        
    }
}
