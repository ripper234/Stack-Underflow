#region

using System;
using NHibernate;
using NHibernate.Criterion;
using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    // ReSharper disable AccessToStaticMemberViaDerivedType

    public class QuestionsRepository : RepositoryBase<Question>, IQuestionsRepository
    {
        public QuestionsRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public Question[] GetNewestQuestions(int numberOfQuestions)
        {
            return Question.SlicedFindAll(0, numberOfQuestions, new[] { Order.Desc("UpdateDate") });
        }

        protected override void ValidateOnSave(Question entity)
        {
            base.ValidateOnSave(entity);
            if (entity.UpdateDate == default(DateTime))
                throw new Exception("Date not valid for question " + entity);
        }
    }

    // ReSharper restore AccessToStaticMemberViaDerivedType
}