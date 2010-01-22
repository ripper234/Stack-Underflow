using System;
using System.Collections.Generic;
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

        public override void FixtureSetupCore()
        {
            _answersRepository = Container.Resolve<IAnswersRepository>();
            _answers = new List<Answer>();
        }

        public override void SetupCore()
        {
            _questionAuthor = UserFactory.CreateUser();
            _question = SaveQuestion(_questionAuthor);
            for (int i = 0; i < 5; ++i)
            {
                var answer = new Answer
                {
                    Author = _questionAuthor,
                    Body = "Answer # " + i,
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
        public void Foo()
        {
            var answers = _answersRepository.GetTopAnswers(_question.Id, 0, 5);
            Assert.AreEqual(5, answers.Count);
            CollectionAssert.IsOrdered(answers);
        }
    }
}
