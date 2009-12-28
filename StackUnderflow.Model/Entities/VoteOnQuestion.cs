using System;
using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities
{
    [ActiveRecord("VotesOnQuestions")]
    public class VoteOnQuestion : ActiveRecordBase<VoteOnQuestion>
    {
        [CompositeKey]
        public VoteKey Key { get; set; }

        [Property]
        public VoteType Vote { get; set; }
    }
}