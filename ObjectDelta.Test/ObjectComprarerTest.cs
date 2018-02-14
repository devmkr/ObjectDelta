using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace ObjectDelta.Test
{
  [TestFixture]
  public class ObjectComprarerTest
  {
    [Test]
    public void Comparer_SameObjectsWithNoComplex_ReturnsNullDelta()
    {
      //arrange
      var fstObject = new ExampleComplesClass
      {
        CoverId = 2,
        IsDismounted = false,
        LightingId = 1
      };
      var sndObject = new ExampleComplesClass
      {
        CoverId = 2,
        IsDismounted = false,
        LightingId = 1
      };
      //assert
      var result = new ObjectComparer().Compare(fstObject, sndObject);
      //act
      Assert.NotNull(result);
      Assert.IsNull(result.Deltas);
    }

    [Test]
    public void Comparer_DiffrentObjectsWithNoComplex_ReturnsDelta()
    {
      //arrange
      var fstObject = new ExampleComplesClass
      {
        CoverId = 1,
        IsDismounted = false,
        LightingId = 1
      };
      var sndObject = new ExampleComplesClass
      {
        CoverId = 2,
        IsDismounted = true,
        LightingId = 1
      };

      var expected = new ObjectDelta<ExampleComplesClass>(sndObject, new List<PropertyDelta>
      {
        new PropertyDelta
        {
          Value = false,
          DataType = typeof(bool),
          Name = nameof(ExampleComplesClass.IsDismounted)
        },
        new PropertyDelta
        {
          Value = 1,
          DataType = typeof(int?),
          Name = nameof(ExampleComplesClass.CoverId)
        }
      });
      //assert
      var result = new ObjectComparer().Compare(fstObject, sndObject);
      //act
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Deltas);
      Assert.That(result.Deltas, Has.Count.EqualTo(2));

      Assert.AreEqual(result.NewValue, expected.NewValue);
      CollectionAssert.AreEqual(result.Deltas, expected.Deltas);
    }

    [Test]
    public void Calculate_EmptyDelta_ReturnsInput()
    {
      //arrange
      var input = new ExampleComplesClass()
      {
        CoverId = 2,
        IsDismounted = false,
        LightingId = 1
      };
      //act
      var result = new ObjectComparer().Calculate(new ObjectDelta<ExampleComplesClass>(input, new List<PropertyDelta>()));
      //assert
      Assert.IsNotNull(result);
      Assert.AreEqual(input, result);
    }

    [Test]
    public void Calculate_DeltaWithProperties_ReturnsTransformedInput()
    {
      //arrange
      var input = new ExampleComplesClass()
      {
       TypeId = 1,
        CoverId = 4,
        SerialNumber = "!231"
      };
      var delta = new ObjectDelta<ExampleComplesClass>(input, new List<PropertyDelta>()
      {
        new PropertyDelta()
        {
          DataType = typeof(int?),
          Name = nameof(ExampleComplesClass.CoverId),
          Value = null,

        },
        new PropertyDelta()
        {
          DataType = typeof(int),
          Name = nameof(ExampleComplesClass.TypeId),
          Value = 3,

        }
      });
      var expected = new ExampleComplesClass()
      {
        TypeId = 3,
        CoverId = null,
        SerialNumber = "!231"
      };

      //act
      var result = new ObjectComparer().Calculate(delta);
      //assert
      Assert.IsNotNull(result);
      Assert.AreEqual(expected, result);
    }

  }
}
