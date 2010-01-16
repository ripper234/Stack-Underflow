using System;
using System.Collections.Generic;
using System.Linq;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
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

        public RichQuestion GetById(User viewingUser, int questionId)
        {
            var question = _questionsRepository.GetById(questionId);
            var votes = _voteRepository.GetVoteCount(questionId);
            VoteType? vote = null;
            if (viewingUser != null)
            {
                var voteOnQuestion = _voteRepository.GetVote(viewingUser.Id, questionId);
                if (voteOnQuestion != null)
                    vote = voteOnQuestion.Vote;
            }
            return new RichQuestion(question, votes.Total, vote);
        }

        public List<RichQuestion> GetNewestQuestions(int numberOfQuestions)
        {
            var questions = _questionsRepository.GetNewestQuestions(numberOfQuestions);
            var votes = _voteRepository.GetVoteCount(questions.Select(q => q.Id));
            return questions.Select(q => new RichQuestion(q, votes.GetOrDefault(q.Id), null)).ToList();
        }
    }
}