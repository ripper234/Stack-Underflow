#region

using Castle.Core.Resource;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
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

        public WindsorContainer GetContainer()
        {
            var container = CreateContainer();
            // InitializeActiveRecord();
            return container;
        }

//        private static void InitializeActiveRecord()
//        {
//            ActiveRecordStarter.Initialize(typeof(User).Assembly, ActiveRecordSectionHandler.Instance);
//        }

        private WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer(new XmlInterpreter(new FileResource("castle.xml")));
//            var foo = new XmlInterpreter("");
//            IConfigurationStore
//            var config = new XmlConfigurationSource("castle.xml");
//            var container = new WindsorContainer();
            //container.Installer.SetUp(container, config);
            container.AutoWireServicesIn(typeof (IUserRepository).Assembly);
            // avoid recreating session factory every test, bind it to a single instance
//            container.Register(Component
//                                   .For(typeof (ISessionFactory))
//                                   .Instance(_sessionFactory));
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