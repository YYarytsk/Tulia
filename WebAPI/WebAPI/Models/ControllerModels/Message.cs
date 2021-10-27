using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ControllerModels
{
    public class Message
    {
        public string status { get; set; }
        public string message { get; set; }

        public Message(string status, string message)
        {
            this.status = status;
            this.message = message;
        }
    }
}
