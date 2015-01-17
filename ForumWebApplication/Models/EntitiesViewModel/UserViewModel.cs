using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interfaces;

namespace ForumWebApplication.Models 
{
    public class UserViewModel: IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public List<string> Roles { get; set; }
        public string Avatar { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Pass { get; set; }
    }
}