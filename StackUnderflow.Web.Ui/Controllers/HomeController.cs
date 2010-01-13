using System.Web.Mvc;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class HomeController : UserAwareController
    {
        private readonly IQuestionsRepository _questionsRepository;

        public HomeController(IUserRepository userRepository, 
                                 IQuestionsRepository questionsRepository) : base(userRepository)
        {
            _questionsRepository = questionsRepository;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return UserView(_questionsRepository.GetNewestQuestions(10));
        }
    }
}
