#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate;
using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected RepositoryBase(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }

        protected ISessionFactory SessionFactory
        {
            get; private set;
        }

        public virtual T GetById(int id)
        {
            return ActiveRecordBase<T>.Find(id);
        }

        /// <summary>
        /// This adds (saves) a new record.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Save(T entity) 
        {
            ActiveRecordMediator<T>.Save(entity);
        }

        protected static void BuildInClause(StringBuilder builder, IEnumerable<int> ids)
        {
            if (ids.Count() == 0)
                throw new Exception("Can't build in clause for empty ID list");

            builder.Append("IN (");
            bool first = true;
            foreach (var id in ids)
            {
                if (first)
                {
                    first = false;
                    builder.Append(id);
                }
                else
                    builder.Append(", ").Append(id);
            }
            builder.Append(") ");
        }

        protected Dictionary<int, int> RunQuery(string sql, Func<IGrouping<object, object[]>, int> valueSelector)
        {
            using (var session = SessionFactory.OpenSession())
            {
                var query = session.CreateQuery(sql);
                var result = query.List().Cast<object[]>().GroupBy(x => x[0]);
                return result.ToDictionary(x1 => (int)x1.Key, valueSelector);
            }
        }
    }
}