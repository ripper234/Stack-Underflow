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
        private readonly IAnswerVoteRepository _answerVoteRepository;

        public RichQuestionRepository(IQuestionsRepository questionsRepository, IQuestionVoteRepository questionVoteRepository, IAnswersRepository answersRepository, IAnswerVoteRepository answerVoteRepository)
        {
            _questionsRepository = questionsRepository;
            _questionVoteRepository = questionVoteRepository;
            _answersRepository = answersRepository;
            _answerVoteRepository = answerVoteRepository;
        }

        public RichQuestion GetById(User viewingUser, int questionId, int answerStart, int numAnswers)
        {
            var question = _questionsRepository.GetById(questionId);
            var votesOnQuestions = _questionVoteRepository.GetVoteCount(questionId);
            VoteType? vote = null;
            if (viewingUser != null)
            {
                var voteOnQuestion = _questionVoteRepository.GetVote(viewingUser.Id, questionId);
                if (voteOnQuestion != null)
                    vote = voteOnQuestion.Vote;
            }
            var answers = _answersRepository.GetTopAnswers(questionId, answerStart, numAnswers);
            var answerCount = _answersRepository.GetAnswerCount(questionId);
            var answerIDs = answers.Select(x => x.Id);
            var votesOnAnswers = viewingUser == null ? null : _answerVoteRepository.GetVotes(viewingUser.Id, answerIDs);
            return new RichQuestion(question, votesOnQuestions.Total, vote, CreateRichAnswers(answers, votesOnAnswers), answerCount);
        }
        
        public List<RichQuestion> GetNewestQuestions(int numberOfQuestions)
        {
            var questions = _questionsRepository.GetNewestQuestions(numberOfQuestions);
            var questionIds = questions.Select(q => q.Id).ToList();
            var votes = _questionVoteRepository.GetVoteCount(questionIds);
            var answerCounts = _answersRepository.GetAnswerCount(questionIds);
            return questions.Select(QuestionCreateor(votes, answerCounts)).ToList();
        }

        private static Func<Question, RichQuestion> QuestionCreateor(Dictionary<int, int> votes,
                                                                     Dictionary<int, int> answerCounts)
        {
            return q => new RichQuestion(q, 
                                         votes.GetOrDefault(q.Id), 
                                         null, 
                                         null, 
                                         answerCounts.GetOrDefault(q.Id));
        }

        private static List<RichAnswer> CreateRichAnswers(IList<Answer> answers,
                                                          Dictionary<int, VoteType> votesOnAnswers)
        {
            return answers.Select(x => new RichAnswer(x, 
                votesOnAnswers.GetOrNull(x.Id))).ToList();
        }

    }
}