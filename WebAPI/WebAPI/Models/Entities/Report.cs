using System;
using System.Collections.Generic;

#nullable disable

namespace WebAPI.Models.Entities
{
    public partial class Report
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Type { get; set; }
        public string ReportContent { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
