using System;
using NHibernate;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

namespace StackUnderflow.Util
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = Bootstrap.Bootstrapper.Instance.GetContainer();
                var userRepository = container.Resolve<IUserRepository>();
                var user = new User {Name = "Ron"};
                userRepository.Save(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
            }
        }
    }
}
