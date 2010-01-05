using System;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Tests.Persistence
{
    public class UserFactory : IUserFactory
    {
        private readonly IUserRepository _userRepository;
        private int _id;
        public UserFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateUser()
        {
            var user = new User
                           {
                               Name = "User" + _id, 
                               SignupDate = DateTime.Now,
                               OpenId = "OpenId" + _id,

                           };
            ++_id;
            _userRepository.Save(user);
            return user;
        }
    }

    public interface IUserFactory
    {
        User CreateUser();
    }
}