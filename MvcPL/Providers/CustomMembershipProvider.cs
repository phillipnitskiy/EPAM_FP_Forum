using System;
using System.Web.Helpers;
using System.Web.Security;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.Input;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        private IUserService UserService => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        private IRoleService RoleService => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public MembershipUser CreateUser(UserRegistrationInputModel userModel)
        {
            MembershipUser membershipUser = GetUser(userModel.Login, false);

            if (membershipUser != null)
                return null;

            var userEntity = userModel.ToBllUser();

            UserService.CreateUser(userEntity);

            Roles.AddUserToRole(userModel.Login, "User");

            membershipUser = GetUser(userModel.Login, false);
            return membershipUser;
        }

        public override bool ValidateUser(string login, string password)
        {
            return UserService.ValidateUser(login, password);
        }

        public override MembershipUser GetUser(string login, bool userIsOnline)
        {
            var user = UserService.GetUserEntityByLogin(login);

            if (user == null)
                return null;

            var memberUser = new MembershipUser(
                "CustomMembershipProvider", user.Login,
                user.Email, null, null, null,
                false, false, DateTime.Now,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override string GetUserNameByEmail(string email)
        {
            var user = UserService.GetUserEntityByEmail(email);

            if (user == null)
                return null;

            return user.Login;
        }

        #region Stabs

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
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

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }

        #endregion
    }
}