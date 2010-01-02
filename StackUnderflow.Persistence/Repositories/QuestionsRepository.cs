#region

using System;
using Castle.ActiveRecord;
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
            return ActiveRecordMediator<Question>.SlicedFindAll(0, numberOfQuestions, new[] { Order.Desc("UpdateDate") });
        }

        public override void Save(Question question)
        {
            if (question.UpdateDate == default(DateTime) ||
                question.AskedOn == default(DateTime))
                throw new Exception("Date not valid for question " + question);

            question.LastRelatedUser = question.Author;
            ActiveRecordMediator<Question>.Save(question);
        }
    }

    // ReSharper restore AccessToStaticMemberViaDerivedType
}