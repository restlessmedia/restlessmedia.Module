using Autofac;
using restlessmedia.Module.Caching;
using restlessmedia.Module.Configuration;
using restlessmedia.Module.Data;
using SqlBuilder.DataServices;

namespace restlessmedia.Module
{
  public class ModuleBuilder
  {
    public static void RegisterModules(ContainerBuilder containerBuilder)
    {
      RegisterComponents(containerBuilder);
      ModuleLoader<IModule>.Load(x => x.RegisterComponents(containerBuilder));
    }

    private static void RegisterComponents(ContainerBuilder containerBuilder)
    {
      #region settings
      containerBuilder.RegisterSettings<ILicenseSettings>("restlessmedia/license", required: true);
      containerBuilder.RegisterSettings<IDatabaseSettings>("restlessmedia/database", required: true);
      containerBuilder.RegisterSettings<ICacheSettings>("restlessmedia/cache");
      #endregion

      #region contexts
      containerBuilder.RegisterType<ConnectionFactory>().As<IConnectionFactory>().SingleInstance();
      containerBuilder.RegisterType<SqlRetry>().As<IRetry>().SingleInstance();
      containerBuilder.RegisterType<DataContext>().As<IDataContext>().SingleInstance();
      #endregion

      #region services
      containerBuilder.RegisterType<EntityService>().As<IEntityService>().SingleInstance();
      #endregion

      #region providers
      containerBuilder.RegisterType<PubSubProvider>().As<IPubSubProvider>().SingleInstance();

      // this is the default cache provider, most applications override with redis cache which is done via json config
      containerBuilder.RegisterType<HttpCacheProvider>().As<ICacheProvider>().SingleInstance();
      #endregion

      #region Data
      containerBuilder.RegisterGeneric(typeof(ModelDataProvider<>)).As(typeof(IModelDataProvider<>));
      containerBuilder.RegisterGeneric(typeof(ModelDataService<>)).As(typeof(IModelDataService<>));
      containerBuilder.RegisterGeneric(typeof(ModelDataService<,>)).As(typeof(IModelDataService<,>));
      containerBuilder.RegisterType<EntityDataProvider>().As<IEntityDataProvider>().SingleInstance();
      #endregion
    }
  }
}