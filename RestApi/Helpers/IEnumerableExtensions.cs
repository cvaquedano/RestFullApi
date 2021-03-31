using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace RestApi.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var expandoObjectList = new List<ExpandoObject>();

            var propertyInfoList = new List<PropertyInfo>();

            var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            propertyInfoList.AddRange(propertyInfos);

            foreach (TSource sourceObject in source)
            {
                var dataShapeObject = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(sourceObject);

                    ((IDictionary<string, object>)dataShapeObject).Add(propertyInfo.Name, propertyValue);
                }
                expandoObjectList.Add(dataShapeObject);

            }

            return expandoObjectList;
        }

    }
}
