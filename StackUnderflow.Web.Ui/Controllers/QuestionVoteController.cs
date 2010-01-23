using System.Web.Mvc;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class QuestionVoteController : VoteController<Question, VoteOnQuestion>
    {
        public QuestionVoteController(IUserRepository userRepository, IQuestionVoteRepository voteRepository) : base(userRepository, voteRepository)
        {
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ProcessVote(int postId, VoteType voteType, bool wasOn)
        {
            return ProcessVoteImpl(postId, voteType, wasOn);
        }
    }
}