using System;
using System.Collections.Generic;
using System.Linq;
using StackUnderflow.Common;
using StackUnderflow.Model.Entities;
using StackUnderflow.Model.Entities.DB;
using StackUnderflow.Model.Entities.Rich;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Persistence.RichRepositories
{
    public class RichQuestionRepository : IRichQuestionRepository
    {
        private readonly IQuestionsRepository _questionsRepository;
        private readonly IQuestionVoteRepository _questionVoteRepository;
        private readonly IAnswersRepository _answersRepository;

        public RichQuestionRepository(IQuestionsRepository questionsRepository, IQuestionVoteRepository questionVoteRepository, IAnswersRepository answersRepository)
        {
            _questionsRepository = questionsRepository;
            _questionVoteRepository = questionVoteRepository;
            _answersRepository = answersRepository;
        }

        public RichQuestion GetById(User viewingUser, int questionId, long answerStart, int numAnswers)
        {
            var question = _questionsRepository.GetById(questionId);
            var votes = _questionVoteRepository.GetVoteCount(questionId);
            VoteType? vote = null;
            if (viewingUser != null)
            {
                var voteOnQuestion = _questionVoteRepository.GetVote(viewingUser.Id, questionId);
                if (voteOnQuestion != null)
                    vote = voteOnQuestion.Vote;
            }
            var answers = _answersRepository.GetTopAnswers(questionId, answerStart, numAnswers);
            return new RichQuestion(question, votes.Total, vote, answers);
        }

        public List<RichQuestion> GetNewestQuestions(int numberOfQuestions)
        {
            var questions = _questionsRepository.GetNewestQuestions(numberOfQuestions);
            var votes = _questionVoteRepository.GetVoteCount(questions.Select(q => q.Id));
            return questions.Select(q => new RichQuestion(q, votes.GetOrDefault(q.Id), null, null)).ToList();
        }
    }
}