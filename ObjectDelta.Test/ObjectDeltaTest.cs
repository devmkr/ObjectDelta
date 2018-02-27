using System.Collections.Generic;
using NUnit.Framework;

namespace ObjectDelta.Test
{
  [TestFixture]
  public class ObjectDeltaTest
  {
    [Test]
    public void Equals_Samecollection_ReturnsTrue()
    {
      //arrange
      var snd = new ObjectDelta<ExampleComplexClass>(new ExampleComplexClass()
      {
        CoverId = 1
      }, new List<PropertyDelta>()
      {
        new PropertyDelta()
        {
          DataType = typeof(int),
          Name = "test",
          Value = 1,
        }
      } );

      //act
      var result = new ObjectDelta<ExampleComplexClass>(new ExampleComplexClass()
      {
        CoverId = 1
      }, new List<PropertyDelta>()
      {
        new PropertyDelta()
        {
          DataType = typeof(int),
          Name = "test",
          Value = 1,
        }
      }).Equals(snd);

      //assert
      Assert.IsTrue(result);
    }

    [Test]
    public void Equals_DiffCollection_ReturnsFalse()
    {
      //arrange
      var snd = new ObjectDelta<ExampleComplexClass>(new ExampleComplexClass()
      {
        CoverId = 1
      }, new List<PropertyDelta>()
      {
        new PropertyDelta()
        {
          DataType = typeof(int),
          Name = "test",
          Value = 1,
        }
      });

      //act
      var result = new ObjectDelta<ExampleComplexClass>(new ExampleComplexClass()
      {
        CoverId = 1
      }, new List<PropertyDelta>()
      {
        new PropertyDelta()
        {
          DataType = typeof(int),
          Name = "test",
          Value = 2,
        }
      }).Equals(snd);

      //assert
      Assert.IsFalse(result);
    }
  }
}