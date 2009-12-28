using System;
using System.Linq;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using NHibernate;
using NHibernate.Criterion;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public class VoteRepository : RepositoryBase<VoteOnQuestion>, IVoteRepository
    {
        private ISessionFactory _sessionFactory;

        public VoteRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void AddVote(User user, Question question, VoteType voteType)
        {
            var vote = new VoteOnQuestion
                {
                    Key = new VoteKey {UserId = user.Id, QuestionId = question.Id},
                    Vote = voteType
                };
            vote.Create();
        }

        public VoteCount GetVoteCount(int questionId)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    var query =
                        session.CreateQuery(
                            "SELECT vote, COUNT(*) FROM votesonquestions WHERE questionid = :questionId GROUP BY vote");
                    query.SetInt32("questionId", questionId);
                    var result = query.List().Cast<object[]>().ToDictionary(x => (VoteType) x[0], x => (int) x[1]);
                    return new VoteCount(result.TryGetValueWithDefault(VoteType.ThumbUp),
                                         result.TryGetValueWithDefault(VoteType.ThumbDown));
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to get vote count for question " + questionId, e);
            }

//            try
//            {
//                var query = new CountQuery(typeof (VoteOnQuestion));
//                
//                var up = new ScalarQuery<int>(typeof (VoteOnQuestion),
//                                              "SELECT vote, COUNT(*) FROM votesonquestions WHERE questionid = 1 GROUP BY vote").
//                    Execute();
//                var down = new ScalarQuery<int>(typeof (VoteOnQuestion),
//                                                "select count(*) where QuestionID = " + questionId + " and Vote = " + 1)
//                    .Execute();
//                return new VoteCount(up, down);
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Failed to get vote count for question " + questionId, e);
//            }
//            var thumbUps = (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
//             where vote.Question.Id == questionId && vote.Vote == VoteType.ThumbUp 
//                        select vote);
//            var thumbDowns = (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
//                            where vote.Question.Id == questionId && vote.Vote == VoteType.ThumbUp
//                            select vote);
//
//            return new VoteCount
//                       {
//                           ThumbUps = 0,
//                           ThumbDowns = 1,
//                       };
//         
//            (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
//             where vote.Vote == VoteType.ThumbUp && 
//             vote.Question.Id == questionId
//             group vote by vote.Vote into g
//            select new {g.}
//             ).Count();
//            var foo = new LinqQuery<VoteOnQuestion>();
//            LinqQuery<VoteOnQuestion>.
//            Linq<VoteOnQuestion>
//            VoteOnQuestion.
//            CountQuery query = new CountQuery(typeof (VoteOnQuestion));
//            query.
//            ActiveRecordMediator<VoteOnQuestion>.Count(Expression.Gt("date", DateTime.Now))
        }

        public VoteOnQuestion GetVote(User user, Question question)
        {
            return ActiveRecordBase<VoteOnQuestion>.Find(new VoteKey(user.Id, question.Id));
        }
    }
}