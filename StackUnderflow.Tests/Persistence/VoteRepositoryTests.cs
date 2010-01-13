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
            var user = UserFactory.CreateUser();
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
            var askingUser = UserFactory.CreateUser();
            var thumbUpper1 = UserFactory.CreateUser();
            var thumbUpper2 = UserFactory.CreateUser();
            var thumbDowner = UserFactory.CreateUser();

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
            var user = UserFactory.CreateUser();
            var question = SaveQuestion(user);

            var voteCount = VoteRepository.GetVoteCount(question.Id);
            Assert.AreEqual(0, voteCount.ThumbUps);
            Assert.AreEqual(0, voteCount.ThumbDowns);
        }

        [Test]
        public void BulkCount()
        {
            var author = UserFactory.CreateUser();
            var voter1 = UserFactory.CreateUser();
            var voter2 = UserFactory.CreateUser();
            var voter3 = UserFactory.CreateUser();
            var question1 = SaveQuestion(author);
            var question2 = SaveQuestion(author);

            VoteRepository.AddVote(voter1, question1, VoteType.ThumbUp);
            VoteRepository.AddVote(voter2, question1, VoteType.ThumbUp);
            VoteRepository.AddVote(voter3, question1, VoteType.ThumbDown);

            VoteRepository.AddVote(voter1, question2, VoteType.ThumbDown);
            VoteRepository.AddVote(voter2, question2, VoteType.ThumbDown);

            var votes = VoteRepository.GetVoteCount(new[] {question1.Id, question2.Id});
            Assert.AreEqual(2, votes.Count);
            Assert.AreEqual(1, votes[question1.Id]);
            Assert.AreEqual(-2, votes[question2.Id]);
        }
    }
}