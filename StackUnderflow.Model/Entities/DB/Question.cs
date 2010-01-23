#region

using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities.DB
{
    [ActiveRecord]
    public class Question : Post
    {
        [Property]
        public string Title { get; set; }
    }
}