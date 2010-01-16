#region

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public class VoteRepository : RepositoryBase<VoteOnQuestion>, IVoteRepository
    {
        public VoteRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        #region IVoteRepository Members

        public void CreateOrUpdateVote(User user, Question question, VoteType voteType)
        {
            CreateOrUpdateVote(user.Id, question.Id, voteType);
        }

        public void CreateOrUpdateVote(int userId, int questionId, VoteType voteType)
        {
            var vote = new VoteOnQuestion
            {
                Key = new VoteKey { UserId = userId, QuestionId = questionId },
                Vote = voteType
            };

            // http://stackoverflow.com/questions/2077107/insert-or-update-in-castle-activerecord
            try
            {
                ActiveRecordMediator<VoteOnQuestion>.Create(vote);
            }
            catch (Exception e)
            {
                ActiveRecordMediator<VoteOnQuestion>.Update(vote);
            }
        }

        public void RemoveVote(int voter, int question)
        {
            var voteOnQuestion = new VoteOnQuestion {Key = new VoteKey(voter, question)};
            try
            {
                ActiveRecordMediator<VoteOnQuestion>.Delete(voteOnQuestion);
            }
            catch (Exception)
            {
                // 
            }
        }

        public VoteCount GetVoteCount(int questionId)
        {
            try
            {
                using (var session = SessionFactory.OpenSession())
                {
                    var query =
                        session.CreateQuery(
                            "SELECT Vote, COUNT(*) FROM VoteOnQuestion WHERE QuestionId = :questionId GROUP BY vote");
                    query.SetInt32("questionId", questionId);
                    var result = query.List().Cast<object[]>().ToDictionary(
                        x => (VoteType) x[0],
                        x => (long) x[1]);
                    return new VoteCount(result.TryGetValueWithDefault(VoteType.ThumbUp),
                                         result.TryGetValueWithDefault(VoteType.ThumbDown));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get vote count for question " + questionId, e);
            }
        }

        public Dictionary<int, int> GetVoteCount(IEnumerable<int> questionIds)
        {
            try
            {
                var questionIdsStr = "";
                bool first = true;
                foreach (var id in questionIds)
                {
                    if (first)
                    {
                        first = false;
                        questionIdsStr += id;
                    }
                    else
                        questionIdsStr += ", " + id;
                }
                var sql = string.Format("SELECT Key.QuestionId, Vote, COUNT(*) FROM VoteOnQuestion WHERE QuestionId IN ({0}) GROUP BY questionid, vote", questionIdsStr);
                using (var session = SessionFactory.OpenSession())
                {
                    var query = session.CreateQuery(sql);
                    var result = query.List().Cast<object[]>().GroupBy(x => x[0]);
                    return result.ToDictionary(x => (int)x.Key, 
                                                  x => x.Sum(y => (int)(((long)y[2]) * GetWeight((VoteType)y[1]))));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get vote counts for questions " + questionIds, e);
            }
        }

        private static int GetWeight(VoteType voteType)
        {
            switch (voteType)
            {
                case VoteType.ThumbUp:
                    return 1;

                case VoteType.ThumbDown:
                    return -1;

                default:
                    throw new Exception("Unsupported vote type: " + voteType);
            }
        }

        public VoteOnQuestion GetVote(User user, Question question)
        {
            return GetVote(user.Id, question.Id);
        }

        /// <summary>
        /// Returns null if no vote exists
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public VoteOnQuestion GetVote(int userId, int questionId)
        {
            return ActiveRecordBase<VoteOnQuestion>.TryFind(new VoteKey(userId, questionId));
        }

        #endregion
    }
}