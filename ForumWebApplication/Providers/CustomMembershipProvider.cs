using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common;
using System.Web.Helpers;
using System.Web.Security;
using ForumWebApplication.Models;
using Bll.Interface.Entities;
using Bll.Interface.DataServices;
using ForumWebApplication.App_Start;
using DependencyResolver;

namespace ForumWebApplication.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {

        public MembershipUser CreateUser(UserViewModel viewUser)
        {
            MembershipUser membershipUser = GetUser(viewUser.Login, false);

            if (membershipUser != null)
            {
                return null;
            }
            IKernel kernel = NinjectWebCommon.CreateKernel();
            using (IUserService userService = kernel.Get<IUserService>())
            {
                using (IRoleService roleService = kernel.Get<IRoleService>())
                {
                    var user = new User
                    {
                        Mail = viewUser.Mail,
                        Name = viewUser.Name,
                        Login = viewUser.Login,
                        Pass = Crypto.HashPassword(viewUser.Pass),
                        Avatar = viewUser.Avatar,
                        RegistrationDate = viewUser.RegistrationDate,
                        Roles = new List<Role>()
                        //http://msdn.microsoft.com/ru-ru/library/system.web.helpers.crypto(v=vs.111).aspx
                    };

                    Role role = roleService.GetByName("user");
                    if (role != null)
                    {
                        user.Roles.Add(role);
                    }
                    userService.Add(user);
                    userService.SetUserRoles(user);
                }
            }
            membershipUser = GetUser(viewUser.Login, false);
            return membershipUser;


        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            IKernel kernel = NinjectWebCommon.CreateKernel();
            using (IUserService userService = kernel.Get<IUserService>())
            {
                var user = userService.GetByLogin(login);

                if (user == null) return null;
                var memberUser = new MembershipUser("CustomMembershipProvider", user.Login, user.Mail,
                    null, null, null,
                    false, false, user.RegistrationDate, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                return memberUser;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved,
            object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}