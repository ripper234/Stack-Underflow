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
            VoteRepository.CreateOrUpdateVote(user, question, VoteType.ThumbUp);
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
            VoteRepository.CreateOrUpdateVote(thumbUpper1, question, VoteType.ThumbUp);
            VoteRepository.CreateOrUpdateVote(thumbUpper2, question, VoteType.ThumbUp);
            VoteRepository.CreateOrUpdateVote(thumbDowner, question, VoteType.ThumbDown);

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

            VoteRepository.CreateOrUpdateVote(voter1, question1, VoteType.ThumbUp);
            VoteRepository.CreateOrUpdateVote(voter2, question1, VoteType.ThumbUp);
            VoteRepository.CreateOrUpdateVote(voter3, question1, VoteType.ThumbDown);

            VoteRepository.CreateOrUpdateVote(voter1, question2, VoteType.ThumbDown);
            VoteRepository.CreateOrUpdateVote(voter2, question2, VoteType.ThumbDown);

            var votes = VoteRepository.GetVoteCount(new[] {question1.Id, question2.Id});
            Assert.AreEqual(2, votes.Count);
            Assert.AreEqual(1, votes[question1.Id]);
            Assert.AreEqual(-2, votes[question2.Id]);
        }
        
        [Test]
        public void UpdatingAVoteTwice_DoesntUpdate_ThenUpdateToDifferetnVoteWorks()
        {
            var author = UserFactory.CreateUser();
            var voter = UserFactory.CreateUser(); 
            var question = SaveQuestion(author);
            VoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            VoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            VoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbDown);
            Assert.AreEqual(-1, TallyVotes(question.Id));
        }

        private int TallyVotes(int questionId)
        {
            var voteCount = VoteRepository.GetVoteCount(questionId);
            return voteCount.ThumbUps - voteCount.ThumbDowns;
        }

        [Test]
        public void RemoveVote_RemovesCorrectVote()
        {
            var author = UserFactory.CreateUser();
            var voter = UserFactory.CreateUser();
            var question = SaveQuestion(author);
            VoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            VoteRepository.RemoveVote(voter.Id, question.Id);
            Assert.AreEqual(0, TallyVotes(question.Id));
        }

        [Test]
        public void GetOnNonExistingVote_ReturnsNull()
        {
            var author = UserFactory.CreateUser();
            var question = SaveQuestion(author);
            Assert.IsNull(VoteRepository.GetVote(author, question));
        }
    }
}