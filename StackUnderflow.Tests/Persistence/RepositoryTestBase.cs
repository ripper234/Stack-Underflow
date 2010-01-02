#region

using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public class RepositoryTestBase : IntegrationTestBase
    {
        public IUserRepository UserRepository { get; private set; }
        public IQuestionsRepository QuestionsRepository { get; private set; }
        public IVoteRepository VoteRepository { get; private set; }

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            UserRepository = Resolve<IUserRepository>();
            QuestionsRepository = Resolve<IQuestionsRepository>();
            VoteRepository = Resolve<IVoteRepository>();
        }

        protected User SaveUser()
        {
            var user = new User {Name = "Chop Sui"};
            UserRepository.Save(user);
            return user;
        }

        protected Question SaveQuestion(User user)
        {
            var question = QuestionsFactory.CreateQuestion(user);
            QuestionsRepository.Save(question);
            return question;
        }
    }
}