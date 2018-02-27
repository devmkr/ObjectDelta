using System;
using System.ComponentModel;
using System.Linq;

namespace ObjectDelta
{
  public class PropertyDelta
  {
    public string Name { get; set; }

    public string Display
    {
      get
      {
        var propertyInfo = DataType.GetProperty(Name);
        if (propertyInfo == null)
          return Name;
        return ((DisplayNameAttribute)propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                 .FirstOrDefault())
               ?.DisplayName ?? Name;
      }
    }

    public object Value { get; set; }

    public Type DataType { get; set; }

    protected bool Equals(PropertyDelta other)
    {
      return string.Equals(Name, other.Name) && Equals(Value, other.Value) && DataType == other.DataType;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((PropertyDelta)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = Name != null ? Name.GetHashCode() : 0;
        hashCode = (hashCode * 397) ^ (Value != null ? Value.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (DataType != null ? DataType.GetHashCode() : 0);
        return hashCode;
      }
    }
  }
}