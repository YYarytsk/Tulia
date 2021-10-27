using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.DBModels
{
    public class MembershipWithGroup
    {
        public MembershipWithGroup() { }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public Entities.Group Group { get; set; }
        public Entities.User User { get; set; }

        public MembershipWithGroup(int Id, int UserId, int GroupId, Entities.Group Group)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.GroupId = GroupId;
            this.Group = Group;
            
        }
    }
}

