using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities.DB
{
    [ActiveRecord]
    public class Answer : Post
    {
        [Property]
        public int QuestionId { get; set; }
    }
}