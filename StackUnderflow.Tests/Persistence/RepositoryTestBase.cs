#region

using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public class RepositoryTestBase : IntegrationTestBase
    {
        public IUserRepository UserRepository { get; private set; }
        public IQuestionsRepository QuestionsRepository { get; private set; }
        public IVoteRepository VoteRepository { get; private set; }
        public IUserFactory UserFactory { get; private set; }

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            UserRepository = Resolve<IUserRepository>();
            QuestionsRepository = Resolve<IQuestionsRepository>();
            VoteRepository = Resolve<IVoteRepository>();
            UserFactory = Resolve<IUserFactory>();
        }

        protected Question SaveQuestion(User user)
        {
            var question = QuestionsFactory.CreateQuestion(user);
            QuestionsRepository.Save(question);
            return question;
        }
    }
}