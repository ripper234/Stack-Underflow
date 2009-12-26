using System;
using Castle.ActiveRecord;
using NHibernate;
using NHibernate.Linq;

namespace StackUnderflow.Persistence
{
    /// <summary>
   /// Provides a LINQ-enabled data context that works with ActiveRecord.
   /// </summary>
    public class ActiveRecordLinqContext : NHibernateContext
    {
        private ActiveRecordLinqContext() : base(GetSession()) { }

        ///<summary>
        ///</summary>
        ///<typeparam name="T"></typeparam>
        ///<returns></returns>
        public static INHibernateQueryable<T> Linq<T>() where T : ActiveRecordBase
        {
            return (SingletonCreator.Instance.Session.Linq<T>());
        }

        private static class SingletonCreator
        {
            internal static readonly ActiveRecordLinqContext Instance = new ActiveRecordLinqContext();
        }

        private static ISession GetSession()
        {
            if (SessionScope.Current == null)
                throw new InvalidOperationException("No active SessionScope found.");

            return ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(ActiveRecordBase));
        }
    }
}
