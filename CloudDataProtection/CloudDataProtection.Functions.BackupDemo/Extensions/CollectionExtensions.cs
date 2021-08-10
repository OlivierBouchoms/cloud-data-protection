using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CloudDataProtection.Functions.BackupDemo.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddIf<T>(this ICollection<T> collection, T obj, bool predicate)
        {
            if (predicate)
            {
                collection.Add(obj);
            }
        }
        
        public static void AddIf<T>(this ICollection<T> collection, T obj, Expression<Func<T, bool>> predicate)
        {
            Func<T, bool> pred = predicate.Compile();
            
            if (pred.Invoke(obj))
            {
                collection.Add(obj);
            }
        }
    }
}