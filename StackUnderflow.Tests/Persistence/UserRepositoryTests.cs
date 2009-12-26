using System;
using NUnit.Framework;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Tests.Persistence
{
    [TestFixture]
    public class UserRepositoryTests : IntegrationTestBase
    {
        private IUserRepository _userRepository;

        public override void FixtureSetupCore()
        {
            _userRepository = Container.Resolve<IUserRepository>();
        }

        [Test]
        public void SavedUserGetsId()
        {
            var user = new User {Name = "Ron"};
            Assert.AreEqual(0, user.Id);
            _userRepository.Save(user);
            Assert.AreNotEqual(0, user.Id);
        }

        [Test]
        public void SavedUsersDetails_AreRetrieved()
        {
            var user = new User { Name = "Ron" , WebsiteUrl = new Uri("http://foo.com")};
            _userRepository.Save(user);
            var savedUser = _userRepository.GetById(user.Id);
            
            Assert.AreEqual("Ron", savedUser.Name);
            Assert.AreEqual("http://foo.com/", savedUser.WebsiteUrl.AbsoluteUri);
        }

        [Test]
        public void SaveUserWithoutWebsite()
        {
            var user = new User { Name = "Ron" };
            _userRepository.Save(user);
            _userRepository.GetById(user.Id);
        }
    }
}