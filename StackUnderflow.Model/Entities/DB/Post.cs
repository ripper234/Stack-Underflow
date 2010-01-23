using System;
using Castle.ActiveRecord;

namespace StackUnderflow.Model.Entities.DB
{
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

        [Property]
        public int Votes { get; set; }
    }
}