using System;
using System.Linq.Expressions;
using System.Reflection;

namespace restlessmedia.Module.Extensions
{
  public static class ExpressionExtensions
  {
    /// <summary>
    /// Gets the value of the field or property from the expression.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProp"></typeparam>
    /// <param name="expression"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static TProp GetValue<T, TProp>(this Expression<Func<T, TProp>> expression, T target)
    {
      MemberInfo memberInfo = GetMemberInfo<T, TProp>(expression);

      if (memberInfo == null)
      {
        return default(TProp);
      }

      return (TProp)memberInfo.GetMemberValue(target);
    }

    /// <summary>
    /// Returns the <see cref="MemberInfo"/> from the expression otherwise null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProp"></typeparam>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static MemberInfo GetMemberInfo<T, TProp>(this Expression<Func<T, TProp>> expression)
    {
      MemberExpression memberExpression = null;

      // this line is necessary, because sometimes the expression comes in as Convert(originalexpression)
      if (expression.Body is UnaryExpression unaryExpression)
      {
        if (unaryExpression.Operand is MemberExpression expression1)
        {
          memberExpression = expression1;
        }
      }
      else if (expression.Body is MemberExpression expression1)
      {
        memberExpression = expression1;
      }

      return memberExpression?.Member;
    }
  }
}