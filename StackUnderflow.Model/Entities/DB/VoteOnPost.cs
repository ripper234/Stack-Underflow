using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities.DB
{
    public abstract class VoteOnPost
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