using System;

namespace ObjectDelta.Test
{
  public class ExampleComplesClass : IEquatable<ExampleComplesClass>
  {
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public int? ModelId { get; set; }
    public int TypeId { get; set; }
    public int? ManufactuerId { get; set; }
    public int? RefrigerantId { get; set; }
    public int? LightingId { get; set; }
    public bool IsDismounted { get; set; }
    public int? CoverId { get; set; }
    public int StateId { get; set; }
    public int? ShopId { get; set; }

    public bool Equals(ExampleComplesClass other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Id == other.Id && string.Equals(SerialNumber, other.SerialNumber) && ModelId == other.ModelId &&
             TypeId == other.TypeId && ManufactuerId == other.ManufactuerId && RefrigerantId == other.RefrigerantId &&
             LightingId == other.LightingId && IsDismounted == other.IsDismounted && CoverId == other.CoverId &&
             StateId == other.StateId && ShopId == other.ShopId;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((ExampleComplesClass) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = Id;
        hashCode = (hashCode * 397) ^ (SerialNumber != null ? SerialNumber.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ ModelId.GetHashCode();
        hashCode = (hashCode * 397) ^ TypeId;
        hashCode = (hashCode * 397) ^ ManufactuerId.GetHashCode();
        hashCode = (hashCode * 397) ^ RefrigerantId.GetHashCode();
        hashCode = (hashCode * 397) ^ LightingId.GetHashCode();
        hashCode = (hashCode * 397) ^ IsDismounted.GetHashCode();
        hashCode = (hashCode * 397) ^ CoverId.GetHashCode();
        hashCode = (hashCode * 397) ^ StateId;
        hashCode = (hashCode * 397) ^ ShopId.GetHashCode();
        return hashCode;
      }
    }
  }
}