using System.Collections.Generic;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IAnswersRepository : IRepository<Answer>
    {
        List<Answer> GetTopAnswers(int questionId, int answerStart, int maxResults);
        Dictionary<int, int> GetAnswerCount(IList<int> questionIds);
        int GetAnswerCount(int questionId);
    }
}