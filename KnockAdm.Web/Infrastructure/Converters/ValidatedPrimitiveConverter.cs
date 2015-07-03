using System;
using Newtonsoft.Json;

namespace KnockAdm.Web
{
    public class ValidatedPrimitiveConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType &&
                   objectType.GetGenericTypeDefinition() == typeof (Validated<>);
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var valueAsString = serializer.Deserialize<string>(reader);
            return Activator.CreateInstance(objectType, valueAsString);
        }
    }
}