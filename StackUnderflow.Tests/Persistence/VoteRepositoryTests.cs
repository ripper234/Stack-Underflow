#region

using NUnit.Framework;
using StackUnderflow.Model.Entities;

#endregion

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
        public void TwoThumbUpsAndOneThumbDown_CountVotesIsCorrect()
        {
            // prepare users
            var askingUser = SaveUser();
            var thumbUpper1 = SaveUser();
            var thumbUpper2 = SaveUser();
            var thumbDowner = SaveUser();

            // and question
            var question = SaveQuestion(askingUser);

            // vote
            VoteRepository.AddVote(thumbUpper1, question, VoteType.ThumbUp);
            VoteRepository.AddVote(thumbUpper2, question, VoteType.ThumbUp);
            VoteRepository.AddVote(thumbDowner, question, VoteType.ThumbDown);

            // count votes
            var voteCount = VoteRepository.GetVoteCount(question.Id);
            Assert.AreEqual(2, voteCount.ThumbUps);
            Assert.AreEqual(1, voteCount.ThumbDowns);
        }

        [Test]
        public void UnvotedQuestion_CountVotesIsZero()
        {
            var user = SaveUser();
            var question = SaveQuestion(user);

            var voteCount = VoteRepository.GetVoteCount(question.Id);
            Assert.AreEqual(0, voteCount.ThumbUps);
            Assert.AreEqual(0, voteCount.ThumbDowns);
        }
    }
}