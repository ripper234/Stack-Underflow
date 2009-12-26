using NHibernate;

namespace Tests
{
    public static class DBUtils
    {
        public static void ClearDatabase(ISessionFactory sessionFactory)
        {
            using (ISession session = sessionFactory.OpenSession())
            using (ITransaction tx = session.BeginTransaction())
            {
                session.CreateSQLQuery("truncate table Users").ExecuteUpdate();
                tx.Commit();
            }
        }
    }
}