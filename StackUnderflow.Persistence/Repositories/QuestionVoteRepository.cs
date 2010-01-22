#region

using System;
using System.Collections.Generic;
using System.Linq;
using Castle.ActiveRecord;
using NHibernate;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public class QuestionVoteRepository : PostVoteRepository<VoteOnQuestion, Question>, IQuestionVoteRepository
    {
        public QuestionVoteRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}