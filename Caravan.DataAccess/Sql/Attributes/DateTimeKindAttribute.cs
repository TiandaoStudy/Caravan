using PommaLabs.Thrower;
using System;
using System.Linq;
using System.Reflection;

namespace Finsa.Caravan.DataAccess.Sql.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DateTimeKindAttribute : Attribute
    {
        public DateTimeKindAttribute(DateTimeKind kind)
        {
            RaiseArgumentException.IfNot(Enum.IsDefined(typeof(DateTimeKind), kind));
            Kind = kind;
        }

        public DateTimeKind Kind { get; }

        public static void Apply(object entity)
        {
            if (entity == null)
            {
                return;
            }

            var properties = entity.GetType().GetProperties()
                .Where(x => x.PropertyType == typeof(DateTime) || x.PropertyType == typeof(DateTime?));

            foreach (var property in properties)
            {
                var attr = property.GetCustomAttribute<DateTimeKindAttribute>();
                if (attr == null)
                {
                    continue;
                }

                var dt = property.PropertyType == typeof(DateTime?)
                    ? (DateTime?)property.GetValue(entity)
                    : (DateTime)property.GetValue(entity);

                if (!dt.HasValue)
                {
                    continue;
                }

                property.SetValue(entity, DateTime.SpecifyKind(dt.Value, attr.Kind));
            }
        }
    }
}
