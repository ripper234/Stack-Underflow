using System;
using System.Web.Mvc;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Utils
{
    public static class UserContainer
    {
        private static readonly string USER_KEY = "User";
        public static IUserRepository UserRepository { get; set; }
        
        public static User GetUser(ViewDataDictionary viewData, string id)
        {
            object userObject;
            if (viewData.TryGetValue(USER_KEY, out userObject))
                return (User) userObject;

            var user = UserRepository.GetById(int.Parse(id));
            viewData[USER_KEY] = user;
            return user;
        }
    }
}