using FastMember;
using System;
using System.Collections.Generic;
using System.Linq;

namespace restlessmedia.Module.Attributes
{
  public static class AttributeHelper
  {
    /// <summary>
    /// Returns members for T which have or don't have the attribute defined
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="isDefined"></param>
    /// <returns></returns>
    public static IEnumerable<Member> Filter<T, TAttribute>(bool isDefined = true)
      where TAttribute : Attribute
    {
      Type type = typeof(T);
      Type attributeType = typeof(TAttribute);
      MemberSet members = TypeAccessor.Create(type).GetMembers();
      Func<Member, bool> predicate;
      
      if (isDefined)
      {
        predicate = (x) => x.IsDefined(attributeType);
      }
      else
      {
        predicate = (x) => !x.IsDefined(attributeType);
      }

      return members.Where(predicate);
    }
  }
}