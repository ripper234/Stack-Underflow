using NHibernate;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public class AnswersRepository : RepositoryBase<Answer>, IAnswersRepository
    {
        public AnswersRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}