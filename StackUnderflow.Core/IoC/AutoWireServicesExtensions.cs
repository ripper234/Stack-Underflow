using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using StackUnderflow.IoC.Delver.Framework.IoC;

namespace StackUnderflow.IoC
{
    /// <summary>
    /// Allow auto wiring of services to the container using conventions
    /// </summary>
    public static class AutoWireServicesExtensions
    {
        /// <summary>
        /// Allow auto wiring of services to the container using conventions
        /// </summary>
        /// <param name="container">The container to register into</param>
        /// <param name="assembly">The assembly in which implementations reside</param>
        /// <returns>The container, with registered services</returns>
        public static IWindsorContainer AutoWireServicesIn(this IWindsorContainer container, Assembly assembly)
        {
            return AutoWireServicesIn(container, assembly, new Type[0]);
        }

        /// <summary>
        /// Allow auto wiring of services to the container using conventions
        /// </summary>
        /// <param name="container">The container to register into</param>
        /// <param name="assembly">The assembly in which implementations reside</param>
        /// <param name="implementationsToSkip"></param>
        /// <returns>The container, with registered services</returns>
        public static IWindsorContainer AutoWireServicesIn(this IWindsorContainer container, Assembly assembly,
                                                           ICollection<Type> implementationsToSkip)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            var types = from type in assembly.GetExportedTypes()
                                      where type.IsAbstract == false
                                            && type.IsInterface == false
                                            && implementationsToSkip.Contains(type) == false
                                      select type;

            foreach (Type type in types)
            {
                string serviceName = "I" + type.Name;
                LifestyleType lifestyle;

                Type interfaceType =
                    type.GetInterfaces().
                        Where(i => i.GetAttribute<ImplementedByAttribute>() != null).
                        SingleOrDefault();

                if (interfaceType != null)
                {
                    lifestyle = interfaceType.GetAttribute<ImplementedByAttribute>().Lifestyle;
                }
                else
                {
                    lifestyle = LifestyleType.Singleton;
                    interfaceType = (from i in type.GetInterfaces()
                                     where i.Name == serviceName
                                     select i)
                        .SingleOrDefault();

                    if (interfaceType == null)
                        continue;
                }

                ComponentRegistration<object> registration = Component
                    .For(interfaceType)
                    .ImplementedBy(type)
                    .LifeStyle.Is(lifestyle);

                container.Register(registration);
            }

            return container;
        }

        public static T GetAttribute<T>(this Type type)
        {
            return type.GetCustomAttributes(typeof (T), false).
                Cast<T>().SingleOrDefault();
        }
    }
}