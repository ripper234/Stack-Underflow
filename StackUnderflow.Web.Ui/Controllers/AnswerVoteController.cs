using System.Web.Mvc;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class AnswerVoteController : VoteController<Answer, VoteOnAnswer>
    {
        public AnswerVoteController(IUserRepository userRepository, IAnswerVoteRepository voteRepository)
            : base(userRepository, voteRepository)
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcessVote(int postId, VoteType voteType, bool wasOn)
        {
            return ProcessVoteImpl(postId, voteType, wasOn);
        }
    }
}