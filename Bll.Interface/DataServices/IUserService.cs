using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;


namespace Bll.Interface.DataServices
{
    public interface IUserService: IService<User>
    {
        IEnumerable<User> GetByName(string name);
        User GetByLogin(string login);
        void SetUserRoles(User user);
        //IEnumerable<User> GetByRole(Role role);
    }
}
