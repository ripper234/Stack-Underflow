using NHibernate;

namespace Tests
{
    public static class DBUtils
    {
        public static void ClearDatabase(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                session.CreateSQLQuery("truncate table Questions").ExecuteUpdate();
                session.CreateSQLQuery("truncate table Users").ExecuteUpdate();
                tx.Commit();
            }
        }
    }
}