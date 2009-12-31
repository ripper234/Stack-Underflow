#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using StackUnderflow.IoC.Delver.Framework.IoC;

#endregion

namespace StackUnderflow.Common.IoC
{
    /// <summary>
    ///   Allow auto wiring of services to the container using conventions
    /// </summary>
    public static class AutoWireServicesExtensions
    {
        /// <summary>
        ///   Allow auto wiring of services to the container using conventions
        /// </summary>
        /// <param name="container">
        ///   The container to register into
        /// </param>
        /// <param name="assembly">
        ///   The assembly in which implementations reside
        /// </param>
        /// <returns>
        ///   The container, with registered services
        /// </returns>
        public static IWindsorContainer AutoWireServicesIn(this IWindsorContainer container, Assembly assembly)
        {
            return AutoWireServicesIn(container, assembly, new Type[0]);
        }

        /// <summary>
        ///   Allow auto wiring of services to the container using conventions
        /// </summary>
        /// <param name="container">
        ///   The container to register into
        /// </param>
        /// <param name="assembly">
        ///   The assembly in which implementations reside
        /// </param>
        /// <param name="implementationsToSkip"></param>
        /// <returns>
        ///   The container, with registered services
        /// </returns>
        public static IWindsorContainer AutoWireServicesIn(this IWindsorContainer container, Assembly assembly,
                                                           ICollection<Type> implementationsToSkip)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            RegisterServiceInterfaces(container, assembly, implementationsToSkip);
            RegisterControllersInAssembly(container, assembly);
            return container;
        }

        private static void RegisterControllersInAssembly(IWindsorContainer container, Assembly assembly)
        {
            var types = from type in assembly.GetExportedTypes()
                        where typeof (Controller).IsAssignableFrom(type) 
                        select type;
            foreach (var type in types)
            {
                var nameRegex = new Regex("(.*)Controller");
                var match = nameRegex.Match(type.Name);
                if (!match.Success)
                    continue; // we auto-register only controllers named FooController

                var component = Component.For(type).Named(match.Groups[1].Value).LifeStyle.PerWebRequest;
                
                container.Register(component);
            }
        }

        private static void RegisterServiceInterfaces(IWindsorContainer container, Assembly assembly, ICollection<Type> implementationsToSkip)
        {
            var types = from type in assembly.GetExportedTypes()
                        where type.IsAbstract == false
                              && type.IsInterface == false
                              && implementationsToSkip.Contains(type) == false
                        select type;

            foreach (var type in types)
            {
                var serviceName = "I" + type.Name;
                LifestyleType lifestyle;

                var interfaceType =
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

                var registration = Component
                    .For(interfaceType)
                    .ImplementedBy(type)
                    .LifeStyle.Is(lifestyle);

                container.Register(registration);
            }
        }

        public static T GetAttribute<T>(this Type type)
        {
            return type.GetCustomAttributes(typeof (T), false).
                Cast<T>().SingleOrDefault();
        }
    }
}