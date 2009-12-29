#region

using System;
using StackUnderflow.Bootstrap;
using StackUnderflow.Persistence.Entities;
using StackUnderflow.Persistence.Repositories;

#endregion

namespace StackUnderflow.Util
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var container = Bootstrapper.Instance.GetContainer();
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