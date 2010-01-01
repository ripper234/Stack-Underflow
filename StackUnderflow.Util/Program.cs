#region

using System;
using StackUnderflow.Bootstrap;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Util
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var container = Bootstrapper.Instance.GetContainer();
                var userRepository = container.Resolve<IUserRepository>();
                var questionsRepository = container.Resolve<IQuestionsRepository>();

                // save some users and questions
                var ron = new User {Name = "Ron"};
                var aya = new User { Name = "Aya" };
                var miri = new User { Name = "Miri" };
                userRepository.Save(ron);
                userRepository.Save(aya);
                userRepository.Save(miri);

                var question1 = new Question
                                    {
                                        Author = ron,
                                        Title = "Will this work?",
                                        Text = "Well, will?",
                                        UpdateDate = DateTime.Now
                                    };
                var question2 = new Question
                                    {
                                        Author = aya,
                                        Title = "Watch star trek?",
                                        Text = "Can we do it?",
                                        UpdateDate = DateTime.Now.Subtract(TimeSpan.FromMinutes(5))
                                    };
                questionsRepository.Save(question1);
                questionsRepository.Save(question2);

                var questions = questionsRepository.GetNewestQuestions(10);
                Console.WriteLine(string.Format("Got {0} questions", questions.Length));
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                    e = e.InnerException;
                }
            }
        }
    }
}