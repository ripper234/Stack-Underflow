using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Criterion;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        public AnswersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public List<Answer> GetTopAnswers(int questionId, int answerStart, int maxResults)
        {
            var answeres = ActiveRecordMediator<Answer>.SlicedFindAll(answerStart, maxResults, 
                                                       DetachedCriteria.For<Answer>().
                                                        Add(Restrictions.Eq("QuestionId", questionId)), 
                                                       Order.Desc("Votes"));
            return new List<Answer>(answeres);
        }

        public Dictionary<int, int> GetAnswerCount(IList<int> questionIds)
        {
            if (questionIds.Count == 0)
                return new Dictionary<int, int>(0);

            var builder = new StringBuilder("SELECT QuestionId, count(*) from Answer ");
            builder.Append("WHERE QuestionId ");
            BuildInClause(builder, questionIds);
            builder.Append(" GROUP BY QuestionId");

            var sql = builder.ToString();
            return RunQuery(sql, x => (int)(long)(x.Single()[1]));
        }

        public int GetAnswerCount(int questionId)
        {
            var answerCounts = GetAnswerCount(new[] {questionId});
            return answerCounts.GetOrDefault(questionId);
        }
    }
}