#region

using System;
using System.Collections.Generic;
using Castle.ActiveRecord;

#endregion

namespace StackUnderflow.Model.Entities
{
    // ReSharper disable UnusedAutoPropertyAccessor.Local
    // ReSharper disable UnusedMember.Local

    [ActiveRecord]
    public class User
    {
        [PrimaryKey]
        public virtual int Id { get; private set; }

        [Property]
        public virtual string Name { get; set; }

        [HasMany]
        public virtual IList<Question> Questions { get; set; }

        public virtual Uri WebsiteUrl { get; set; }

        [Property("Website")]
        private string WebsiteUrlString
        {
            get
            {
                return WebsiteUrl == null ? null : WebsiteUrl.AbsoluteUri;
            }
            set
            {
                if (value == null)
                {
                    WebsiteUrl = null;
                    return;
                }

                WebsiteUrl = new Uri(value);
            }
        }

        [Property]
        public int Reputation {get; set; }

        [Property]
        public DateTime SignupDate {get; set;}

        [Property]
        public string OpenId { get; set;}
        

// ReSharper restore UnusedMember.Local
// ReSharper restore UnusedAutoPropertyAccessor.Local

    }
}