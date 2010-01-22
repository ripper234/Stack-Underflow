using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        public AnswersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public List<Answer> GetTopAnswers(int questionId, long answerStart, long answerEnd)
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query =
                    session.CreateQuery(
                        "SELECT * from VoteOnAnswer, COUNT(*) FROM VoteOnQuestion WHERE QuestionId = :questionId GROUP BY vote");
                query.SetInt32("questionId", questionId);
                var result = query.List().Cast<object[]>().ToDictionary(
                    x => (VoteType)x[0],
                    x => (long)x[1]);
//                return new VoteCount(result.TryGetValueWithDefault(VoteType.ThumbUp),
//                                     result.TryGetValueWithDefault(VoteType.ThumbDown));
                throw new Exception("TODO");
            }
        }
    }
}