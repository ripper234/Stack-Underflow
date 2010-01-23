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
                AddQuery(session, "truncate table tags");
                AddQuery(session, "truncate table questiontags");
                AddQuery(session, "truncate table VotesOnAnswers");
                AddQuery(session, "truncate table Answers");
                AddQuery(session, "truncate table VotesOnQuestions");
                AddQuery(session, "truncate table Questions");
                AddQuery(session, "truncate table Users");
                tx.Commit();
            }
        }

        private static void AddQuery(ISession session, string query)
        {
            session.CreateSQLQuery(query).ExecuteUpdate();
        }
    }
}