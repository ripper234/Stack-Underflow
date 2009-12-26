using Castle.ActiveRecord;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Model.Entities
{
    [ActiveRecord("VotesOnQuestions")]
    public class VoteOnQuestion : ActiveRecordBase<VoteOnQuestion>
    {
        [PrimaryKey("VoteId")]
        public int Id { get; set; }

        [BelongsTo]
        public User User { get; set; }

        [BelongsTo]
        public Question Question { get; set; }

        [Property]
        public VoteType Vote { get; set; }
    }
}