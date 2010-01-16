using System.Web.Mvc;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class VoteController : UserAwareController
    {
        private readonly IVoteRepository _voteRepository;

        public VoteController(IUserRepository userRepository, IVoteRepository voteRepository) : base(userRepository)
        {
            _voteRepository = voteRepository;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcessVote(int questionId, VoteType voteType, bool wasOn)
        {
            if (CurrentUser == null)
            {
                // Can't vote if not logged in
                return ReturnVal(VoteResult.UserNotLoggedIn);
            }

            if (!wasOn)
                _voteRepository.CreateOrUpdateVote(CurrentUser.Id, questionId, voteType);
            else
                _voteRepository.RemoveVote(CurrentUser.Id, questionId);

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