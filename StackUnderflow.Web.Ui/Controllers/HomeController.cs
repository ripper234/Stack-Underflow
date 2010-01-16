using System.Web.Mvc;
using StackUnderflow.Persistence.Repositories;
using StackUnderflow.Persistence.RichRepositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class HomeController : UserAwareController
    {
        private readonly IRichQuestionRepository _richQuestionsRepository;

        public HomeController(IUserRepository userRepository, 
                                 IRichQuestionRepository richQuestionsRepository) : base(userRepository)
        {
            _richQuestionsRepository = richQuestionsRepository;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return MultipleUserView(_richQuestionsRepository.GetNewestQuestions(10));
        }
    }
}
