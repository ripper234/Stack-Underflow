using System;
using System.Linq;
using StackUnderflow.Model.Entities;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Repositories
{
    public class VoteRepository : RepositoryBase<VoteOnQuestion>, IVoteRepository
    {
        public void AddVote(User user, Question question, VoteType voteType)
        {
            throw new NotImplementedException();
        }

        public VoteCount GetVoteCount(int questionId)
        {
            var thumbUps = (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
             where vote.Question.Id == questionId && vote.Vote == VoteType.ThumbUp 
                        select vote);
            var thumbDowns = (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
                            where vote.Question.Id == questionId && vote.Vote == VoteType.ThumbUp
                            select vote);

            return new VoteCount
                       {
                           ThumbUps = thumbUps.Count(),
                           ThumbDowns = thumbDowns.Count()
                       };
//         
//            (from vote in ActiveRecordLinqContext.Linq<VoteOnQuestion>()
//             where vote.Vote == VoteType.ThumbUp && 
//             vote.Question.Id == questionId
//             group vote by vote.Vote into g
//            select new {g.}
//             ).Count();
//            var foo = new LinqQuery<VoteOnQuestion>();
//            LinqQuery<VoteOnQuestion>.
//            Linq<VoteOnQuestion>
//            VoteOnQuestion.
//            CountQuery query = new CountQuery(typeof (VoteOnQuestion));
//            query.
//            ActiveRecordMediator<VoteOnQuestion>.Count(Expression.Gt("date", DateTime.Now))
        }
    }
}