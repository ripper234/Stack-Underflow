﻿using System;
using System.Web.Mvc;
using StackUnderflow.Model.Entities.DB;
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
            return SingleUserView(_richQuestionRepository.GetById(GetCurrentUser(), id, 0, 10));
        }


        //
        // GET: /Question/Edit/5
 
        public ActionResult Edit(int id)
        {
            return EmptyUserView();
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
                return EmptyUserView();
            }
        }

        public ActionResult Ask()
        {
            if (GetCurrentUser() == null)
                return RedirectToLoginPage();

            return EmptyUserView();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ask(string title, string body)
        {
            if (GetCurrentUser() == null)
                return RedirectToLoginPage();
            var now = DateTime.Now;
            var question = new Question
                {
                    CreatedDate = now,
                    Author = GetCurrentUser(),
                    Body = body,
                    LastRelatedUser = GetCurrentUser(),
                    Title = title,
                    UpdateDate = now,
                };
            _questionsRepository.Save(question);
            return RedirectToAction("Details", new {id = question.Id});
        }

        private ActionResult RedirectToLoginPage()
        {
            return RedirectToAction("Login", "Authentication");
        }
    }
}
