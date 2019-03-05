using System;

namespace restlessmedia.Module
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class IgnoreAttribute : Attribute { }
}