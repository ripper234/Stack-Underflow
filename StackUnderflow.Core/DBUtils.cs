#region

using NHibernate;

#endregion

namespace StackUnderflow
{
    public static class DBUtils
    {
        public static void ClearDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.CreateSQLQuery("truncate table Answers").ExecuteUpdate();
                session.CreateSQLQuery("truncate table VotesOnQuestions").ExecuteUpdate();
                session.CreateSQLQuery("truncate table Questions").ExecuteUpdate();
                session.CreateSQLQuery("truncate table Users").ExecuteUpdate();
                tx.Commit();
            }
        }
    }
}