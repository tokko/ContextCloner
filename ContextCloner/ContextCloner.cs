using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace ContextCloner
{
    public static class ContextCloner
    {
        public static void CloneContextTo<TContext>(this TContext src, TContext target) where TContext : DbContext
        {
            //src.Database.CompatibleWithModel(true);
            //target.Database.CompatibleWithModel(true);
            var propertyInfos = src.GetType().GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>)).ToList();
            var tables = new Queue<PropertyInfo>(propertyInfos);
            while (tables.Any())
            {
                try
                {
                    var table = tables.Peek();
                    dynamic srcTable = table.GetValue(src);
                    dynamic targetTable = table.GetValue(target);

                    targetTable.AddRange(srcTable);
                    target.SaveChanges();
                    tables.Dequeue();
                }
                catch (Exception e)
                {
                    tables.Enqueue(tables.Dequeue());
                }
            }
        }
    }
}
