#region

using System;
using System.Collections.Generic;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

#endregion

namespace StackUnderflow.Persistence.Repositories
{
    public interface IVoteRepository : IRepository<VoteOnQuestion>
    {
        void CreateOrUpdateVote(User user, Question question, VoteType voteType);
        VoteCount GetVoteCount(int questionId);
        VoteOnQuestion GetVote(User user, Question question);
        Dictionary<int, int> GetVoteCount(IEnumerable<int> questionId);
        void CreateOrUpdateVote(int userId, int questionId, VoteType voteType);
        void RemoveVote(int voter, int question);
        VoteOnQuestion GetVote(int userId, int questionId);
    }
}