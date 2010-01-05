#region

using System;
using System.Linq;
using NUnit.Framework;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public class QuestionsRepositoryTests : IntegrationTestBase
    {
        private IQuestionsRepository _questionsRepository;
        private IUserRepository _userRepository;
        private IUserFactory _userFactory;

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            _questionsRepository = Resolve<IQuestionsRepository>();
            _userRepository = Resolve<IUserRepository>();
            _userFactory = Resolve<IUserFactory>();
        }

        [Test]
        public void AddQuestion_IsRetrieved()
        {
            var user = CreateUser();

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
            var user = CreateUser();

            var question = QuestionsFactory.CreateQuestion(user);
            _questionsRepository.Save(question);

            var savedUser = _userRepository.GetById(user.Id);
            Assert.AreEqual(1, savedUser.Questions.Count);

            var savedQuestion = savedUser.Questions.Single();
            Assert.AreEqual(question.Id, savedQuestion.Id);
            Assert.AreEqual(question.Title, savedQuestion.Title);
            Assert.AreEqual(question.Body, savedQuestion.Body);
        }

        [Test]
        public void GetNewestQuestions()
        {
            var user = CreateUser();

            // save some quetsions
            var question1 = QuestionsFactory.CreateQuestion(user);
            question1.UpdateDate = new DateTime(2009, 01, 01);
            _questionsRepository.Save(question1);
            var question2 = QuestionsFactory.CreateQuestion(user);
            question2.UpdateDate = new DateTime(2009, 12, 31);
            _questionsRepository.Save(question2);
            var question3 = QuestionsFactory.CreateQuestion(user);
            question3.UpdateDate = new DateTime(2009, 05, 06);
            _questionsRepository.Save(question3);
            var question4 = QuestionsFactory.CreateQuestion(user);
            question4.UpdateDate = new DateTime(2003, 05, 06);
            _questionsRepository.Save(question4);

            var questions = _questionsRepository.GetNewestQuestions(3);
            Assert.AreEqual(3, questions.Length);
            Assert.AreEqual(question2.Id, questions[0].Id);
            Assert.AreEqual(question3.Id, questions[1].Id);
            Assert.AreEqual(question1.Id, questions[2].Id);
        }

        private User CreateUser()
        {
            return _userFactory.CreateUser();
        }

        [ExpectedException]
        [Test]
        public void SavingQuestionWithDefaultDate_Fails()
        {
            var user = new User {Name = "Foob"};
            _userRepository.Save(user);
            var question = new Question{Author = user, Body = "Asdf", Title = "ASd"};
            _questionsRepository.Save(question);
        }
    }
}