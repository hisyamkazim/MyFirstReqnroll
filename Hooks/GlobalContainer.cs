using Reqnroll.BoDi;
using System;

namespace MyFirstReqnroll.Hooks
{
    public static class GlobalContainer
    {
        private static readonly Lazy<IObjectContainer> _containerInstance =
            new Lazy<IObjectContainer>(() => new ObjectContainer());

        public static IObjectContainer Instance => _containerInstance.Value;

        public static void RegisterInstance<T>(T instance) where T : class
        {
            if (!Instance.IsRegistered<T>())
            {
                Instance.RegisterInstanceAs(instance);
            }
        }

        public static T Resolve<T>() where T : class
        {
            return Instance.Resolve<T>();
        }
    }
}
