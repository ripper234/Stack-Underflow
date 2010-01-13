using System;
using System.Collections.Generic;
using System.Linq;
using StackUnderflow.Model.Entities.Rich;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Persistence.RichRepositories
{
    public class RichQuestionRepository : IRichQuestionRepository
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IVoteRepository _voteRepository;

        public RichQuestionRepository(IQuestionsRepository questionsRepository, IVoteRepository voteRepository)
        {
            _questionsRepository = questionsRepository;
            _voteRepository = voteRepository;
        }

        public RichQuestion GetById(int questionId)
        {
            var question = _questionsRepository.GetById(questionId);
            var votes = _voteRepository.GetVoteCount(questionId);
            return new RichQuestion(question, votes.Total);
        }

        public List<RichQuestion> GetNewestQuestions(int numberOfQuestions)
        {
            var questions = _questionsRepository.GetNewestQuestions(numberOfQuestions);
            var votes = _voteRepository.GetVoteCount(questions.Select(q => q.Id));
            return questions.Select(q => new RichQuestion(q, votes[q.Id])).ToList();
        }
    }
}