using System;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord.Framework.Config;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using StackUnderflow.IoC;
using StackUnderflow.Persistence.Entities;

namespace StackUnderflow.Bootstrap
{
    public class Bootstrapper
    {
        public static readonly Bootstrapper Instance = new Bootstrapper();
        private readonly ISessionFactory _sessionFactory = CreateSessionFactory();

        /// <summary>
        /// Singleton, use Bootstrapper.Instance
        /// </summary>
        private Bootstrapper()
        {
        }

        public WindsorContainer GetContainer()
        {
            var container = CreateContainer();
            InitializeActiveRecord();
            return container;
        }

        private static void InitializeActiveRecord()
        {
            ActiveRecordStarter.Initialize(new ActiveRecordSectionHandler());
        }

        private WindsorContainer CreateContainer()
        {
            var container = new WindsorContainer();
            container.AutoWireServicesIn(typeof (User).Assembly);
            // avoid recreating session factory every test, bind it to a single instance
            container.Register(Component
                                   .For(typeof (ISessionFactory))
                                   .Instance(_sessionFactory));
            return container;
        }

        private static ISessionFactory CreateSessionFactory()
        {
            const string mySqlConnectionParamName = "MySqlDbConnectionString";
            var configurator = new NHibernateConfigurator();
            MySQLConfiguration mySqlConfiguration =
                MySQLConfiguration.Standard.ConnectionString(
                    c => c.FromConnectionStringWithKey(mySqlConnectionParamName));
            Configuration configuration = configurator.Configure(new NHibernateConfig(mySqlConfiguration));
            return configuration.BuildSessionFactory();
        }
    }
}