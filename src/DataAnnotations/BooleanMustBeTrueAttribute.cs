using System.ComponentModel.DataAnnotations;

namespace restlessmedia.Module.DataAnnotations
{
  public class BooleanMustBeTrueAttribute : ValidationAttribute
  {
    public override bool IsValid(object propertyValue)
    {
      return propertyValue != null
          && propertyValue is bool
          && (bool)propertyValue;
    }
  }
}
