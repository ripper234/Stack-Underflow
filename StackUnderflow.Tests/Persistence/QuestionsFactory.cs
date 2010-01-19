#region

using System;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

#endregion

namespace StackUnderflow.Tests.Persistence
{
    public static class QuestionsFactory
    {
        public static Question CreateQuestion(User user)
        {
            var creationDate = DateTime.Now;
            return new Question {Author = user, Title = "Is there a god?", Body = "Well, is there?",
                UpdateDate = creationDate,
                                 CreatedDate = creationDate
            };
        }
    }
}