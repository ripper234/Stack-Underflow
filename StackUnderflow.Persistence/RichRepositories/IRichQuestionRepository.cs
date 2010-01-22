using System.Collections.Generic;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.Rich;

namespace StackUnderflow.Persistence.RichRepositories
{
    public interface IRichQuestionRepository
    {
        List<RichQuestion> GetNewestQuestions(int numberOfQuestions);
        RichQuestion GetById(User viewingUser, int questionId, long answerStart, long answerEnd);
    }
}
