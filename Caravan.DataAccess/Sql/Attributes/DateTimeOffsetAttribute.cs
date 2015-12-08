using PommaLabs.Thrower;
using System;
using System.Linq;
using System.Reflection;

namespace Finsa.Caravan.DataAccess.Sql.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateTimeOffsetAttribute : Attribute
    {
        public short Hours { get; set; }

        public static void Apply(object entity)
        {
            if (entity == null)
            {
                return;
            }

            var properties = entity.GetType().GetProperties()
                .Where(x => x.PropertyType == typeof(DateTimeOffset) || x.PropertyType == typeof(DateTimeOffset?));

            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<DateTimeOffsetAttribute>();
                if (attr == null)
                {
                    continue;
                }

                var dt = property.PropertyType == typeof(DateTimeOffset?)
                    ? (DateTimeOffset?)property.GetValue(entity)
                    : (DateTimeOffset)property.GetValue(entity);

                if (!dt.HasValue)
                {
                    continue;
                }

                var offset = TimeSpan.FromHours(attr.Hours);
                property.SetValue(entity, dt.Value.ToOffset(offset));
            }
        }
    }
}
