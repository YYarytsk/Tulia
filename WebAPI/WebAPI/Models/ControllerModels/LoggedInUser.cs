using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.ControllerModels
{
    public class LoggedInUser
    {
        public string username { get; private set; }
        public string password { get; private set; }

        public LoggedInUser(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
