#region

using System.Reflection;
using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using StackUnderflow.Common.IoC;
using StackUnderflow.IoC;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Bootstrap
{
    public class Bootstrapper
    {
        public static readonly Bootstrapper Instance = new Bootstrapper();
        //private readonly ISessionFactory _sessionFactory = CreateSessionFactory();

        /// <summary>
        ///   Singleton, use Bootstrapper.Instance
        /// </summary>
        private Bootstrapper()
        {
        }

//        private static void InitializeActiveRecord()
//        {
//            ActiveRecordStarter.Initialize(typeof(User).Assembly, ActiveRecordSectionHandler.Instance);
//        }

        public WindsorContainer CreateContainer(params Assembly[] extreaAssemblies)
        {
            var container = new WindsorContainer(new XmlInterpreter(new FileResource("castle.xml")));
            container.AutoWireServicesIn(typeof (IUserRepository).Assembly);

            foreach (var assembly in extreaAssemblies)
            {
                container.AutoWireServicesIn(assembly);
            }

            return container;
        }

//        private static ISessionFactory CreateSessionFactory()
//        {
//            const string mySqlConnectionParamName = "MySqlDbConnectionString";
//            var configurator = new NHibernateConfigurator();
//            MySQLConfiguration mySqlConfiguration =
//                MySQLConfiguration.Standard.ConnectionString(
//                    c => c.FromConnectionStringWithKey(mySqlConnectionParamName));
//            Configuration configuration = configurator.Configure(new NHibernateConfig(mySqlConfiguration));
//            return configuration.BuildSessionFactory();
//        }
    }
}