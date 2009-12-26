using NUnit.Framework;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

namespace Tests.Persistence
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
    }
}