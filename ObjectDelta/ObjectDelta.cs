using System.Collections.Generic;
using System.Linq;

namespace ObjectDelta
{
  public class ObjectDelta<TObject>
  {
    public ObjectDelta(
      TObject newValue,
      IReadOnlyList<PropertyDelta> deltas)
    {
      NewValue = newValue;
      Deltas = deltas;
    }

    public TObject NewValue { get; }

    public IReadOnlyList<PropertyDelta> Deltas { get; }

    protected bool Equals(ObjectDelta<TObject> other)
    {
      return EqualityComparer<TObject>.Default.Equals(NewValue, other.NewValue) && Deltas.OrderBy(t => t.Name).SequenceEqual(other.Deltas.OrderBy(t => t.Name));
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return (EqualityComparer<TObject>.Default.GetHashCode(NewValue) * 397) ^
               (Deltas != null ? Deltas.GetHashCode() : 0);
      }
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ObjectDelta<TObject>)obj);
    }
  }
}