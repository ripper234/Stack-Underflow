using System;
using System.Web.Mvc;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class QuestionController : UserAwareController
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionController(IQuestionsRepository questionsRepository, 
                                        IUserRepository userRepository) : base(userRepository)
        {
            _questionsRepository = questionsRepository;
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id)
        {
            return UserView(_questionsRepository.GetById(id));
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
