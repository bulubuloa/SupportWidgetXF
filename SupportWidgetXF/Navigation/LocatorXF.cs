using System;
using Autofac;

namespace SupportWidgetXF.Navigation
{
    public class LocatorXF
    {
        private IContainer container;
        private ContainerBuilder containerBuilder;

        private static readonly LocatorXF _instance = new LocatorXF();
        public static LocatorXF Instance
        {
            get => _instance;
        }

        public LocatorXF()
        {
            containerBuilder = new ContainerBuilder();
            RegisterType();
        }

        private void RegisterType()
        {
            RegisterTypeDI();
        }

        public virtual void RegisterTypeDI()
        {
            /*
             * Register all model
             */
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            container = containerBuilder.Build();
        }
    }
}