using System;
using System.Collections.Generic;
using NHibernate;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Util
{
    public class DataSeeder : IDataSeeder
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IQuestionVoteRepository _questionVoteRepository;
        private readonly IAnswerVoteRepository _answerVoteRepository;
        private readonly List<User> _users = new List<User>();
        private readonly IAnswersRepository _answersRepository;
        private readonly Random _random = new Random();
        private readonly List<Question> _questions = new List<Question>();
        private readonly List<Answer> _answers = new List<Answer>();
        private readonly string[] _words = new[] { "Dog", "Cat", "Question", "Computer", "Math", "Problem", "Science" };
        
        public DataSeeder(IUserRepository userRepository, IQuestionsRepository questionsRepository, ISessionFactory sessionFactory,
            IQuestionVoteRepository questionVoteRepository, IAnswersRepository answersRepository,
            IAnswerVoteRepository answerVoteRepository)
        {
            _userRepository = userRepository;
            _questionsRepository = questionsRepository;
            _questionVoteRepository = questionVoteRepository;
            _sessionFactory = sessionFactory;
            _answersRepository = answersRepository;
            _answerVoteRepository = answerVoteRepository;
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
            
            AddQuestionVotes();
            AddAnswers();
            AddAnswerVotes();

            var questions = _questionsRepository.GetNewestQuestions(10);
            Console.WriteLine(string.Format("Got {0} questions", questions.Length));
        }

        private void AddAnswers()
        {
            for (int i = 0; i < 10; ++i)
            {
                int questionId = _random.Next(_questions.Count) + 1;
                int authorId = _random.Next(_users.Count) + 1;
                var author = new User { Id = authorId };
                var date = GetRandomDate();
                var answer = new Answer
                                 {
                                     Author = author,
                                     Body = RandomText(20),
                                     CreatedDate = date,
                                     LastRelatedUser = author,
                                     QuestionId = questionId,
                                     UpdateDate = date,
                                 };
                _answers.Add(answer);
                _answersRepository.Save(answer);
            }
        }

        private void AddQuestionVotes()
        {
            _questionVoteRepository.CreateOrUpdateVote(_users[0], _questions[0], VoteType.ThumbUp);
            _questionVoteRepository.CreateOrUpdateVote(_users[1], _questions[0], VoteType.ThumbUp);
            _questionVoteRepository.CreateOrUpdateVote(_users[0], _questions[1], VoteType.ThumbUp);
            _questionVoteRepository.CreateOrUpdateVote(_users[1], _questions[1], VoteType.ThumbDown);
            _questionVoteRepository.CreateOrUpdateVote(_users[2], _questions[1], VoteType.ThumbUp);
            _questionVoteRepository.CreateOrUpdateVote(_users[2], _questions[2], VoteType.ThumbDown);
        }

        private void AddAnswerVotes()
        {
            _answerVoteRepository.CreateOrUpdateVote(_users[1], _answers[0], VoteType.ThumbUp);
            _answerVoteRepository.CreateOrUpdateVote(_users[0], _answers[1], VoteType.ThumbUp);
            _answerVoteRepository.CreateOrUpdateVote(_users[1], _answers[1], VoteType.ThumbUp);
            _answerVoteRepository.CreateOrUpdateVote(_users[2], _answers[2], VoteType.ThumbUp);
            _answerVoteRepository.CreateOrUpdateVote(_users[2], _answers[2], VoteType.ThumbUp);
        }

        private void AddQuestion()
        {
            var author = _users.Random(_random);
            int bodyLength = _random.Next(15) + 5;
            string body = RandomText(bodyLength);
            var creationDate = GetRandomDate();
            var question = new Question
                               {
                                   Author = author,
                                   Title = "Random question about " + RandomWord() + "?",
                                   Body = body + "?",
                                   UpdateDate = creationDate,
                                   CreatedDate = creationDate,
                               };
            _questions.Add(question);
            _questionsRepository.Save(question);

        }

        private string RandomText(int bodyLength)
        {
            string body = "";
            for (int i = 0; i < bodyLength; ++i)
            {
                var word = RandomWord();
                if (i != 0)
                    word = word.ToLower();
                body += word + " ";
            }
            return body;
        }

        private DateTime GetRandomDate()
        {
            return DateTime.Now.Subtract(TimeSpan.FromSeconds(_random.Next(300)));
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

        private string RandomWord()
        {
            return _words.Random(_random);
        }
    }
}