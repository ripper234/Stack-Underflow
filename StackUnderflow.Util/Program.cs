#region

using System;
using StackUnderflow.Bootstrap;

#endregion

namespace StackUnderflow.Util
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var container = Bootstrapper.Instance.CreateContainer(typeof (Program).Assembly);
                
                // clear it all
                container.Resolve<IDataSeeder>().Run();
            }
            catch (Exception e)
            {
                while (e != null)
                {
                    Console.WriteLine(e.Message + Environment.NewLine + e.StackTrace);
                    e = e.InnerException;
                }
            }
        }

        
    }
}