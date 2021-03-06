﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ObjectDelta
{
  public class ObjectComparer
  {
    public ObjectDelta<TObject> Compare<TObject>(TObject oldObj, TObject newObj) where TObject : class
    {
      var result = new List<PropertyDelta>();

      var properties = typeof(TObject).GetProperties().Where(x=> !x.GetCustomAttributes(typeof(NoComparableAttribute), true).Any());

      foreach (var propertyInfo in properties)
      {
        var type = propertyInfo.PropertyType;
        if (IsPropertyCollection(type))
          continue;

        var oldValue = propertyInfo.GetValue(oldObj);
        var newValue = propertyInfo.GetValue(newObj);

        if (!oldValue?.Equals(newValue) ?? newValue != null)
          result.Add(new PropertyDelta
          {
            Name = propertyInfo.Name,
            Value = oldValue,
            DataType = type
          });
      }

      return new ObjectDelta<TObject>(newObj, result.Any() ? result : null);
    }

    public TObject Calculate<TObject>(ObjectDelta<TObject> delta) where TObject : class, IEquatable<TObject>, new()
    {
      var properties = typeof(TObject).GetProperties();
      var input = delta.NewValue;
      var result = new TObject();

      foreach (var propertyInfo in properties)
      {
        var propertyDelta = delta.Deltas.FirstOrDefault(x => x.Name == propertyInfo.Name);

        propertyInfo.SetValue(result, propertyDelta != null ? propertyDelta.Value : propertyInfo.GetValue(input));
      }

      return result;
    }

    private static bool IsPropertyCollection(Type type)
    {
      if (type == typeof(string))
        return false;

      return type.GetInterface(typeof(IEnumerable<>).FullName) != null ||
             type.GetInterface(typeof(IEnumerable).FullName) != null;
    }
  }
}