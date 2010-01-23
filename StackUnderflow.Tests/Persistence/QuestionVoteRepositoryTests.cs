#region

using System;
using Castle.ActiveRecord;
using NUnit.Framework;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    [TestFixture]
    public class QuestionVoteRepositoryTests : RepositoryTestBase
    {
        [Ignore]
        [Test]
        public void SaveProblem()
        {
            var u = new User { SignupDate = DateTime.Now, Name = "Foo", OpenId = "Bar" };
            ActiveRecordMediator<User>.Save(u);
            var q = new Question
            {
                Author = u,
                Body = "",
                CreatedDate = DateTime.Now,
                LastRelatedUser = u,
                Title = "",
                UpdateDate = DateTime.Now
            };
            ActiveRecordMediator<Question>.Save(q);
            var v = new VoteOnQuestion { Key = new VoteKey(u.Id, q.Id) };
            ActiveRecordMediator<VoteOnQuestion>.Save(v);
        }

        [Test]
        public void AddAndReadVote()
        {
            var user = UserFactory.CreateUser();
            var question = SaveQuestion(user);
            QuestionVoteRepository.CreateOrUpdateVote(user, question, VoteType.ThumbUp);
            var vote = QuestionVoteRepository.GetVote(user, question);
            Assert.AreEqual(user.Id, vote.Key.UserId);
            Assert.AreEqual(question.Id, vote.Key.PostId);
            Assert.AreEqual(VoteType.ThumbUp, vote.Vote);

            AssertVotesOnQuestion(1, question);
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
            QuestionVoteRepository.CreateOrUpdateVote(thumbUpper1, question, VoteType.ThumbUp);
            QuestionVoteRepository.CreateOrUpdateVote(thumbUpper2, question, VoteType.ThumbUp);
            QuestionVoteRepository.CreateOrUpdateVote(thumbDowner, question, VoteType.ThumbDown);

            // count votes
            var voteCount = QuestionVoteRepository.GetVoteCount(question.Id);
            Assert.AreEqual(2, voteCount.ThumbUps);
            Assert.AreEqual(1, voteCount.ThumbDowns);

            AssertVotesOnQuestion(1, question);
        }

        [Test]
        public void UnvotedQuestion_CountVotesIsZero()
        {
            var user = UserFactory.CreateUser();
            var question = SaveQuestion(user);

            var voteCount = QuestionVoteRepository.GetVoteCount(question.Id);
            Assert.AreEqual(0, voteCount.ThumbUps);
            Assert.AreEqual(0, voteCount.ThumbDowns);
            AssertVotesOnQuestion(0, question);
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

            QuestionVoteRepository.CreateOrUpdateVote(voter1, question1, VoteType.ThumbUp);
            QuestionVoteRepository.CreateOrUpdateVote(voter2, question1, VoteType.ThumbUp);
            QuestionVoteRepository.CreateOrUpdateVote(voter3, question1, VoteType.ThumbDown);

            QuestionVoteRepository.CreateOrUpdateVote(voter1, question2, VoteType.ThumbDown);
            QuestionVoteRepository.CreateOrUpdateVote(voter2, question2, VoteType.ThumbDown);

            var votes = QuestionVoteRepository.GetVoteCount(new[] {question1.Id, question2.Id});
            Assert.AreEqual(2, votes.Count);
            Assert.AreEqual(1, votes[question1.Id]);
            Assert.AreEqual(-2, votes[question2.Id]);

            AssertVotesOnQuestion(1, question1);

            AssertVotesOnQuestion(-2, question2);
        }
        
        [Test]
        public void UpdatingAVoteTwice_DoesntUpdate_ThenUpdateToDifferetnVoteWorks()
        {
            var author = UserFactory.CreateUser();
            var voter = UserFactory.CreateUser(); 
            var question = SaveQuestion(author);
            QuestionVoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            AssertVotesOnQuestion(1, question);
            QuestionVoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            AssertVotesOnQuestion(1, question);
            QuestionVoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbDown);
            Assert.AreEqual(-1, TallyVotes(question.Id));
            AssertVotesOnQuestion(-1, question);
        }

        [Test]
        public void RemoveVote_RemovesCorrectVote()
        {
            var author = UserFactory.CreateUser();
            var voter = UserFactory.CreateUser();
            var question = SaveQuestion(author);
            QuestionVoteRepository.CreateOrUpdateVote(voter, question, VoteType.ThumbUp);
            Assert.AreEqual(1, TallyVotes(question.Id));
            AssertVotesOnQuestion(1, question); 
            QuestionVoteRepository.RemoveVote(voter.Id, question.Id);
            Assert.AreEqual(0, TallyVotes(question.Id));
            AssertVotesOnQuestion(0, question);
        }

        [Test]
        public void GetOnNonExistingVote_ReturnsNull()
        {
            var author = UserFactory.CreateUser();
            var question = SaveQuestion(author);
            Assert.IsNull(QuestionVoteRepository.GetVote(author, question));
        }

        private void AssertVotesOnQuestion(int exectedVotes, Question question)
        {
            question = QuestionsRepository.GetById(question.Id);
            Assert.AreEqual(exectedVotes, question.Votes);
        }

        private int TallyVotes(int questionId)
        {
            var voteCount = QuestionVoteRepository.GetVoteCount(questionId);
            return voteCount.ThumbUps - voteCount.ThumbDowns;
        }
    }
}