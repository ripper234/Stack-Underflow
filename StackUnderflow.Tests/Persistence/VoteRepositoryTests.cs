using NUnit.Framework;
using StackUnderflow.Model.Entities;

namespace StackUnderflow.Tests.Persistence
{
    [TestFixture]
    public class VoteRepositoryTests : RepositoryTestBase
    {
        [Test]
        public void AddAndReadVote()
        {
            var user = SaveUser();
            var question = SaveQuestion(user);
            VoteRepository.AddVote(user, question, VoteType.ThumbUp);
            var vote = VoteRepository.GetVote(user, question);
            Assert.AreEqual(user.Id, vote.Key.UserId);
            Assert.AreEqual(question.Id, vote.Key.QuestionId);
            Assert.AreEqual(VoteType.ThumbUp, vote.Vote);
        }

        [Test]
        public void UnvotedQuestion_CountVotesIsZero()
        {
            var user = SaveUser();
            var question = SaveQuestion(user);

            Assert.Equals(0, VoteRepository.GetVoteCount(question.Id));
        }
    }
}
