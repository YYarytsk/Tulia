using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class Membership
    {
        public Membership()
        {
        }

        public Membership(int Id, int UserId, int GroupId)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.GroupId = GroupId;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

    }
}
