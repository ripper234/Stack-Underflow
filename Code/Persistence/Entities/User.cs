using System.Collections.Generic;

namespace StackUnderflow.Persistence.Entities
{
    public class User
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual IList<Question> Questions { get; set; }
        public virtual string Website { get; set; }
    }
}