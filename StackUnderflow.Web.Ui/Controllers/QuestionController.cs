using System;
using System.Web.Mvc;
using StackUnderflow.Persistence.Repositories;
using StackUnderflow.Persistence.RichRepositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class QuestionController : UserAwareController
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IRichQuestionRepository _richQuestionRepository;

        public QuestionController(IQuestionsRepository questionsRepository, 
                                        IUserRepository userRepository,
                                IRichQuestionRepository richQuestionRepository) : base(userRepository)
        {
            _questionsRepository = questionsRepository;
            _richQuestionRepository = richQuestionRepository;
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id)
        {
            return UserView(_richQuestionRepository.GetById(id));
        }


        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            return UserView();
        }

        //
        // POST: /Question/Edit/5

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                throw new NotImplementedException();
                // return RedirectToAction("Index");
            }
            catch
            {
                return UserView();
            }
        }
    }
}
