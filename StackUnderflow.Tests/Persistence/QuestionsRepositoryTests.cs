#region

using System.Linq;
using NUnit.Framework;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public class QuestionsRepositoryTests : IntegrationTestBase
    {
        private IQuestionsRepository _questionsRepository;
        private IUserRepository _userRepository;

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            _questionsRepository = Resolve<IQuestionsRepository>();
            _userRepository = Resolve<IUserRepository>();
        }

        [Test]
        public void AddQuestion_IsRetrieved()
        {
            var user = new User {Name = "Jibi"};
            _userRepository.Save(user);

            var question = QuestionsFactory.CreateQuestion(user);
            _questionsRepository.Save(question);
            Assert.AreNotEqual(0, question.Id);

            var savedQuestion = _questionsRepository.GetById(question.Id);
            Assert.AreEqual(user.Id, savedQuestion.Author.Id);
            Assert.AreEqual(question.Title, savedQuestion.Title);
        }

        [Test]
        public void AddQuestion_GetAllUserQuestions()
        {
            var user = new User {Name = "Foo"};
            _userRepository.Save(user);

            var question = QuestionsFactory.CreateQuestion(user);
            _questionsRepository.Save(question);

            var savedUser = _userRepository.GetById(user.Id);
            Assert.AreEqual(1, savedUser.Questions.Count);

            var savedQuestion = savedUser.Questions.Single();
            Assert.AreEqual(question.Id, savedQuestion.Id);
            Assert.AreEqual(question.Title, savedQuestion.Title);
            Assert.AreEqual(question.Text, savedQuestion.Text);
        }
    }
}