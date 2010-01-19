#region

using System;
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

    public abstract class Post
    {
        [PrimaryKey]
        public int Id { get; set; }
        
        [Property]
        public string Body { get; set; }

        [BelongsTo]
        public User Author { get; set; }

        [Property]
        public DateTime UpdateDate { get; set; }

        [BelongsTo]
        public User LastRelatedUser { get; set;}

        [Property]
        public DateTime CreatedDate { get; set; }
    }
}