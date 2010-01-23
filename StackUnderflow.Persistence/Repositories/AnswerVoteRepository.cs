using NHibernate;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public class AnswerVoteRepository : PostVoteRepository<VoteOnAnswer, Answer>, IAnswerVoteRepository
    {
        public AnswerVoteRepository(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }
    }
}