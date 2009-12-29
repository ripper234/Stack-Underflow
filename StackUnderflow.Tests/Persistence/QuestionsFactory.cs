#region

using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Entities;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public static class QuestionsFactory
    {
        public static Question CreateQuestion(User user)
        {
            return new Question {Author = user, Title = "Is there a god?", Text = "Well, is there?"};
        }
    }
}