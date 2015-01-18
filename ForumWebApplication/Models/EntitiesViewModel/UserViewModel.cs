using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ForumWebApplication.Models 
{
    public class UserViewModel: IEntity
    {
        public long Id { get; set; }

        [Display(Name = "Имя пользователя")]
        public string Name { get; set; }

        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Display(Name = "Электронная почта")]
        public string Mail { get; set; }

        public List<string> Roles { get; set; }

        [Display(Name = "Аватарка")]
        public string Avatar { get; set; }

        [Display(Name = "Дата регистрации")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Пароль")]
        public string Pass { get; set; }
    }
}