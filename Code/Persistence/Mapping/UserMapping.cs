using FluentNHibernate.Mapping;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Persistence.Mapping
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            Table("Users");
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Website);
            // References
        }
    }

    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}