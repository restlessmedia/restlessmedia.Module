using System;

namespace restlessmedia.Module.Attributes
{
  public class BindAsAttribute : Attribute
  {
    public BindAsAttribute(Type modelType)
    {
      ModelType = modelType;
    }

    public Type ModelType { get; private set; }
  }
}