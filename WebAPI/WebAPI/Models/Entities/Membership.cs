using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models.Entities
{
    public partial class Membership
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}
