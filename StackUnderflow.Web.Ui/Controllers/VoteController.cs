using System.Web.Mvc;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public abstract class VoteController<TPost, TVoteOnPost> : UserAwareController
        where TPost : Post where TVoteOnPost : VoteOnPost
    {
        private readonly IPostVoteRepository<TVoteOnPost, TPost> _voteRepository;

        protected VoteController(IUserRepository userRepository,
                                 IPostVoteRepository<TVoteOnPost, TPost> voteRepository)
            : base(userRepository)
        {
            _voteRepository = voteRepository;
        }

        protected ActionResult ProcessVoteImpl(int postId, VoteType voteType, bool wasOn)
        {
            if (GetCurrentUser() == null)
            {
                // Can't vote if not logged in
                return ReturnVal(VoteResult.UserNotLoggedIn);
            }

            if (!wasOn)
                _voteRepository.CreateOrUpdateVote(GetCurrentUser().Id, postId, voteType);
            else
                _voteRepository.RemoveVote(GetCurrentUser().Id, postId);

            return ReturnVal(VoteResult.OK);
        }


        private static ActionResult ReturnVal(object result)
        {
            return new JsonResult {Data = result};
        }
    }

    public enum VoteResult
    {
        OK = 0,
        UserNotLoggedIn = 1,
    }
}