#region

using System;
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

        public VoteOnQuestion GetVote(User user, Question question)
        {
            return ActiveRecordBase<VoteOnQuestion>.Find(new VoteKey(user.Id, question.Id));
        }

        #endregion
    }
}