#region

using Castle.ActiveRecord;
using StackUnderflow.Persistence.Entities;

#endregion

namespace StackUnderflow.Model.Entities
{
    [ActiveRecord]
    public class Question : ActiveRecordBase<Question>
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property]
        public string Text { get; set; }

        [BelongsTo]
        public User Author { get; set; }
    }
}