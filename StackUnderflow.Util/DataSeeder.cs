using System;
using System.Collections.Generic;
using NHibernate;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Util
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly List<User> _users = new List<User>();
        private readonly Random _random = new Random();

        public DataSeeder(IUserRepository userRepository, IQuestionsRepository questionsRepository, ISessionFactory sessionFactory)
        {
            _userRepository = userRepository;
            _questionsRepository = questionsRepository;
            _sessionFactory = sessionFactory;
        }

        public void Run()
        {
            DBUtils.ClearDatabase(_sessionFactory);

            // save some users and questions
            AddUsers("Ron", "Aya", "Miri");
            
            const int numberOfQuestions = 20;
            for (int i = 0; i < numberOfQuestions; ++i)
            {
                AddQuestion();
            }
            
            var questions = _questionsRepository.GetNewestQuestions(10);
            Console.WriteLine(string.Format("Got {0} questions", questions.Length));
        }

        private void AddQuestion()
        {
            var author = _users.Random(_random);
            int bodyLength = _random.Next(15) + 5;
            string body = "";
            for (int i = 0; i < bodyLength; ++i)
            {
                var word = RandomWord();
                if (i != 0)
                    word = word.ToLower();
                body += word + " ";
            }
            var creationDate = DateTime.Now.Subtract(TimeSpan.FromSeconds(_random.Next(300)));
            var question = new Question
                               {
                                   Author = author,
                                   Title = "Random question about " + RandomWord() + "?",
                                   Body = body + "?",
                                   UpdateDate = creationDate,
                                   AskedOn = creationDate,
                               };
            _questionsRepository.Save(question);

        }

        private void AddUsers(params string []names)
        {
            foreach (var name in names)
            {
                var user = new User
                               {
                                   Name = name, 
                                   SignupDate = DateTime.Now.Subtract(TimeSpan.FromHours(_random.Next(200))),
                                   OpenId = "abc" + _random.Next(1000000),
                               };
                _userRepository.Save(user);
                _users.Add(user);
            }
        }

        private readonly string[] _words = new[] {"Dog", "Cat", "Question", "Computer", "Math", "Problem", "Science"};
        private string RandomWord()
        {
            return _words.Random(_random);
        }
    }
}