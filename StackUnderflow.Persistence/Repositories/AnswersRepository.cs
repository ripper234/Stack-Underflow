using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        public AnswersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public List<Answer> GetTopAnswers(long questionId, long answerStart, int maxResults)
        {
            var answeres = ActiveRecordMediator<Answer>.SlicedFindAll((int)answerStart, maxResults, 
                                                       DetachedCriteria.For<Answer>().
                                                        Add(Restrictions.Eq("QuestionId", questionId)), 
                                                       Order.Desc("Votes"));
            return new List<Answer>(answeres);
//                                   
//            using (var session = SessionFactory.OpenSession())
//            {
//                var query =
//                    session.CreateQuery(
//                        "SELECT * from Answers FROM VoteOnAnswer WHERE PostId = :postId");
//                query.SetInt32("postId", postId);
//                var result = query.List().Cast<object[]>().ToDictionary(
//                    x => (VoteType)x[0],
//                    x => (long)x[1]);
//                return new VoteCount(result.TryGetValueWithDefault(VoteType.ThumbUp),
//                                     result.TryGetValueWithDefault(VoteType.ThumbDown));
//                throw new Exception("TODO");
//            }
        }
    }
}