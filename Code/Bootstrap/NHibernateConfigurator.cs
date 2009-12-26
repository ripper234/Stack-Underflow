using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using StackUnderflow.Persistence.Mapping;

namespace StackUnderflow.Bootstrap
{
    public class NHibernateConfigurator
    {
        public Configuration Configure(NHibernateConfig nhconfig)
        {
            return Fluently.Configure()
                .Database(nhconfig.PersistenceConfigurer)
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<UserMapping>())
                .ExposeConfiguration(c => c.Properties.Add("hbm2ddl.keywords", "none"))
                
                .BuildConfiguration()
                
                .SetProperty(Environment.ProxyFactoryFactoryClass,
                             "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle")
                .SetProperty(Environment.ShowSql, "false");
        }
    }
}