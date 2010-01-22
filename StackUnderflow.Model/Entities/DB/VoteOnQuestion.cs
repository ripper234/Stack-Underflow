#region

using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities.DB
{
    [ActiveRecord("VotesOnQuestions")]
    public class VoteOnQuestion : VoteOnPost
    {
        
    }
}