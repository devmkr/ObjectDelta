using System;

namespace ObjectDelta
{
  [AttributeUsage(AttributeTargets.Property)]
  public class NoComparableAttribute : Attribute
  {
    
  }
}