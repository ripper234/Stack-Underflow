using System;
using System.Web.Mvc;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class AnswerController : UserAwareController
    {
        private readonly IAnswersRepository _answersRepository;
        public AnswerController(IUserRepository userRepository, IAnswersRepository answersRepository) : base(userRepository)
        {
            _answersRepository = answersRepository;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Submit(long questionId, string body)
        {
            if (CurrentUser == null)
            {
                // todo - handle casual user
                throw new Exception("Must be signed in to post answers");
            }

            var now = DateTime.Now;
            var answer = new Answer {Body = body, QuestionId = questionId, Author = CurrentUser, CreatedDate = now, LastRelatedUser = CurrentUser, UpdateDate = now};
            _answersRepository.Save(answer);
            return RedirectToAction("Details", "Question", new { id = questionId});
        }
    }
}