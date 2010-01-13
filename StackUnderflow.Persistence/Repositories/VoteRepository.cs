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

        public void AddVote(User user, Question question, VoteType voteType)
        {
            var vote = new VoteOnQuestion
                           {
                               Key = new VoteKey {UserId = user.Id, QuestionId = question.Id},
                               Vote = voteType
                           };
            ActiveRecordMediator<VoteOnQuestion>.Create(vote);
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
            return ActiveRecordBase<VoteOnQuestion>.Find(new VoteKey(user.Id, question.Id));
        }

        #endregion
    }
}