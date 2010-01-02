#region

using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities
{
    [ActiveRecord("VotesOnQuestions")]
    public class VoteOnQuestion
    {
        [CompositeKey]
        public VoteKey Key { get; set; }

        [Property]
        public VoteType Vote { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, Key: {1}, Vote: {2}", base.ToString(), Key, Vote);
        }
    }
}