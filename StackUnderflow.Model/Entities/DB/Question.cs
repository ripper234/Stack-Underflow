#region

using System;
using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities
{
    [ActiveRecord]
    public class Question
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Property]
        public string Title { get; set; }

        [Property]
        public string Body { get; set; }

        [BelongsTo]
        public User Author { get; set; }

        [Property]
        public DateTime UpdateDate { get; set; }

        [BelongsTo]
        public User LastRelatedUser {get; set;}

        [Property]
        public DateTime AskedOn { get; set; }
    }
}