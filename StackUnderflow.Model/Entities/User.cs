#region

using System;
using System.Collections.Generic;
using Castle.ActiveRecord;
using StackUnderflow.Model.Entities;

#endregion

namespace StackUnderflow.Persistence.Entities
{
    [ActiveRecord]
    public class User : ActiveRecordBase<User>
    {
        [PrimaryKey]
        public virtual int Id { get; private set; }

        [Property]
        public virtual string Name { get; set; }

        [HasMany]
        public virtual IList<Question> Questions { get; set; }

        public virtual Uri WebsiteUrl { get; set; }

// ReSharper disable UnusedMember.Local
        [Property("Website")]
        private string WebsiteUrlString
        {
            get
            {
                return WebsiteUrl == null
                           ?
                               null
                           :
                               WebsiteUrl.AbsoluteUri;
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

// ReSharper restore UnusedMember.Local
    }
}