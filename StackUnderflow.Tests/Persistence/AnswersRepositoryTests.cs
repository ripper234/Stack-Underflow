using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Tests.Persistence
{
    [TestFixture]
    public class AnswersRepositoryTests : RepositoryTestBase
    {
        private IAnswersRepository _answersRepository;
        private Question _question;
        private User _questionAuthor;
        private List<Answer> _answers;

        protected override bool CleanSchemaBetweenTests { get { return false; } }

        public override void FixtureSetupCore()
        {
            base.FixtureSetupCore();
            _answersRepository = Container.Resolve<IAnswersRepository>();
            _answers = new List<Answer>();

            CreateData();
        }

        private void CreateData()
        {
            _questionAuthor = UserFactory.CreateUser();
            _question = SaveQuestion(_questionAuthor);
            for (int i = 0; i < 5; ++i)
            {
                var answer = new Answer
                {
                    Author = _questionAuthor,
                    Body = i.ToString(),
                    CreatedDate = DateTime.Now,
                    LastRelatedUser = _questionAuthor,
                    QuestionId = _question.Id,
                    UpdateDate = DateTime.Now
                };
                _answersRepository.Save(answer);
                _answers.Add(answer);

                // add i up-votes for answer i
                for (int j = 0; j < i; ++j)
                {
                    var voter = UserFactory.CreateUser();
                    AnswerVoteRepository.CreateOrUpdateVote(voter, answer, VoteType.ThumbUp);
                }
            }
        }

        [Test]
        public void AnswerCount()
        {
            var answers = _answersRepository.GetAnswerCount(new []{_question.Id});
            Assert.AreEqual(1, answers.Count);
            Assert.AreEqual(_question.Id, answers.Single().Key);
            Assert.AreEqual(5, answers.Single().Value);
        }


        [Test]
        public void OderTest()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 0, 5);
            Assert.AreEqual(5, answers.Count);
            CollectionAssert.AreEqual(new[] { 4, 3, 2, 1, 0 }, GetAnswerBodiesAsInts(answers));
        }

        [Test]
        public void HighLimitTest()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 0, 10);
            Assert.AreEqual(5, answers.Count);
            CollectionAssert.AreEqual(new[] { 4, 3, 2, 1, 0 }, GetAnswerBodiesAsInts(answers));
        }

        [Test]
        public void LowLimitTest()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 0, 3);
            Assert.AreEqual(3, answers.Count);
            CollectionAssert.AreEqual(new[]{4,3,2}, GetAnswerBodiesAsInts(answers));
        }

        [Test]
        public void StartAtLowLimitTest()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 1, 3);
            Assert.AreEqual(3, answers.Count);
            CollectionAssert.AreEqual(new[] { 3, 2, 1 }, GetAnswerBodiesAsInts(answers));
        }

        [Test]
        public void StartAtHighLimitTest()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 1, 10);
            Assert.AreEqual(4, answers.Count);
            CollectionAssert.AreEqual(new[] { 3, 2, 1, 0 }, GetAnswerBodiesAsInts(answers));
        }

        private static IEnumerable<int> GetAnswerBodiesAsInts(List<Answer> answers)
        {
            return answers.Select(a => int.Parse(a.Body));
        }
    }
}
