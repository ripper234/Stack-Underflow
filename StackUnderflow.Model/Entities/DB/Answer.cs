using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities.DB
{
    [ActiveRecord]
    public class Answer : Post
    {
        [Property]
        public long QuestionId { get; set; }
    }
}