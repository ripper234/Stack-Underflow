using System.Collections.Generic;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IAnswersRepository : IRepository<Answer>
    {
        List<Answer> GetTopAnswers(long questionId, long answerStart, int maxResults);
    }
}