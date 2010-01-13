using System.Collections.Generic;
using StackUnderflow.Model.Entities.Rich;

namespace StackUnderflow.Persistence.RichRepositories
{
    public interface IRichQuestionRepository
    {
        RichQuestion GetById(int questionId);
        List<RichQuestion> GetNewestQuestions(int numberOfQuestions);
    }
}
