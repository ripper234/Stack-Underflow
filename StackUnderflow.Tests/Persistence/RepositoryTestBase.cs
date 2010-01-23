#region

using System;
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
        public IQuestionVoteRepository QuestionVoteRepository { get; private set; }
        public IUserFactory UserFactory { get; private set; }
        public IAnswerVoteRepository AnswerVoteRepository { get; private set; }

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            UserRepository = Resolve<IUserRepository>();
            QuestionsRepository = Resolve<IQuestionsRepository>();
            QuestionVoteRepository = Resolve<IQuestionVoteRepository>();
            UserFactory = Resolve<IUserFactory>();
            AnswerVoteRepository = Resolve<IAnswerVoteRepository>();
        }

        protected Question SaveQuestion(User user)
        {
            var question = QuestionsFactory.CreateQuestion(user);
            QuestionsRepository.Save(question);
            return question;
        }
    }
}