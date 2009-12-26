using System;
using System.Collections.Generic;
using Castle.ActiveRecord;

namespace StackUnderflow.Persistence.Entities
{
    [ActiveRecord("Users")]
    public class User : ActiveRecordBase<User>
    {
        [PrimaryKey]
        public virtual int Id { get; private set; }

        [Property]
        public virtual string Name { get; set; }

        //[Property]
        public virtual IList<Question> Questions { get; set; }

        public virtual Uri WebsiteUrl { get; set; }

// ReSharper disable UnusedMember.Local
        [Property("Website")]
        private string WebsiteUrlString
        {
            get
            {
                Console.WriteLine("Get called");
                return WebsiteUrl == null ? 
                    null :
                    WebsiteUrl.AbsoluteUri;
            }
            set
            {
                Console.WriteLine("Set called with " + value);
                WebsiteUrl = new Uri(value);
            }
        }
// ReSharper restore UnusedMember.Local
    }
}