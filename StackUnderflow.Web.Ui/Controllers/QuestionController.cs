using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Web.Ui.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionController(IQuestionsRepository questionsRepository)
        {
            _questionsRepository = questionsRepository;
        }

        //
        // GET: /Question/Details/5

        public ActionResult Details(int id)
        {
            return View(_questionsRepository.GetById(id));
        }

        //
        // GET: /Question/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Question/Create

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                throw new NotImplementedException();
                //return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
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
                return View();
            }
        }
    }
}
