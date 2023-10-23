using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataNoSQL
{
    public static class ObjectHelper
    {
      
        public static object GetPropertyValue<T>(dynamic obj, string propertyName)
        {
            return typeof(T).GetProperty(propertyName).GetValue(obj);
        }

    }
}
