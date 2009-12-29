#region

using System;
using Castle.Core;

#endregion

namespace StackUnderflow.IoC
{
    namespace Delver.Framework.IoC
    {
        /// <summary>
        ///   Specifying hints for container registration.
        ///   When used in conjunction with the AutoWireServicesIn extension method - allow overriding the defaults
        /// </summary>
        [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
        public class ImplementedByAttribute : Attribute
        {
            public ImplementedByAttribute(Type serviceType)
            {
                ServiceType = serviceType;

                // default
                Lifestyle = LifestyleType.Singleton;
            }

            /// <summary>
            ///   The component's lifestyle. The default is Singleton
            /// </summary>
            public LifestyleType Lifestyle { get; set; }

            /// <summary>
            ///   The type of the service (usually an interface) this component is implementing
            /// </summary>
            public Type ServiceType { get; set; }
        }
    }

    [MyAttribute]
    public class MyAttribute : Attribute
    {
        public MyAttribute()
        {
            Console.WriteLine("Constructing myattribute");
        }
    }
}