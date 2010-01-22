using System.Collections.Generic;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;

namespace StackUnderflow.Persistence.Repositories
{
    public interface IPostVoteRepository<TVoteOnPost, TPost> : IRepository<TVoteOnPost> 
        where TPost : Post 
        where TVoteOnPost : VoteOnPost
        
    {
        void CreateOrUpdateVote(User user, TPost post, VoteType voteType);
        VoteCount GetVoteCount(int postId);
        TVoteOnPost GetVote(User user, TPost question);
        Dictionary<int, int> GetVoteCount(IEnumerable<int> postIds);
        void CreateOrUpdateVote(int userId, int postId, VoteType voteType);
        void RemoveVote(int voterId, int postId);
        TVoteOnPost GetVote(int userId, int postId);
    }
}