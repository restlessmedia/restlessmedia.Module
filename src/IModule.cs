using Autofac;

namespace restlessmedia.Module
{
  public interface IModule
  {
    void RegisterComponents(ContainerBuilder containerBuilder);
  }
}