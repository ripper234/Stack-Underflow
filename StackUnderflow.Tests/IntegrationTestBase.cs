#region

using Castle.Windsor;
using NHibernate;
using NUnit.Framework;
using StackUnderflow.Bootstrap;
using StackUnderflow.Tests.Persistence;

#endregion

namespace StackUnderflow.Tests
{
    public abstract class IntegrationTestBase
    {
        protected virtual bool CleanSchemaBetweenTests { get { return true; } }
        public WindsorContainer Container { get; set; }

        [TestFixtureSetUp]
        public virtual void FixtureSetup()
        {
            Container = Bootstrapper.Instance.CreateContainer(typeof(IntegrationTestBase).Assembly);

            if (!CleanSchemaBetweenTests)
                CleanDB();
            FixtureSetupCore();
        }

        [SetUp]
        public virtual void Setup()
        {
            if (CleanSchemaBetweenTests)
                CleanDB();

            SetupCore();
        }
        
        public virtual void FixtureSetupCore()
        {
        }

        public virtual void SetupCore()
        {
        }

        protected T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        private void CleanDB()
        {
            DBUtils.ClearDatabase(Container.Resolve<ISessionFactory>());
        }
    }
}