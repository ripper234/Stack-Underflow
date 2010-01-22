#region

using System;
using StackUnderflow.Model.Entities.DB;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public interface IQuestionVoteRepository : IPostVoteRepository<VoteOnQuestion, Question>
    {
    }
}