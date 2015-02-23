using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Finsa.Caravan.Common.Serialization.Converters
{
    public sealed class IntToBoolConverter : JsonConverter
    {
        private const string LowerTrue = "true";
        private const string LowerFalse = "false";

        private static readonly Dictionary<Type, Func<object, bool>> ConvertibleTypes = new Dictionary<Type, Func<object, bool>>
        {
            {typeof(short), x => ((short) x) != 0},
            {typeof(int), x => ((int) x) != 0},
            {typeof(long), x => ((long) x) != 0L},
            {typeof(byte), x => ((byte) x) != 0U},
            {typeof(ushort), x => ((ushort) x) != 0U},
            {typeof(uint), x => ((uint) x) != 0U},
            {typeof(ulong), x => ((ulong) x) != 0UL},
        };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var @true = ConvertibleTypes[value.GetType()](value);
            writer.WriteValue(@true ? LowerTrue : LowerFalse);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (reader.Value.ToString().ToLower() == LowerTrue) ? 1 : 0;
        }

        public override bool CanConvert(Type objectType)
        {
            return ConvertibleTypes.ContainsKey(objectType);
        }
    }
}