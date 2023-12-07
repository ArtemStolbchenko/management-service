using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using management_service.Model;

namespace management_service.Utilities
{
    public abstract class AbstractJsonConverter<T> : JsonConverter
    { //I hate this thing, but it seems that there is no way to deserealize an array of abstract classes without it
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            T target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(
            JsonWriter writer,
            object value,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected static bool FieldExists(
            JObject jObject,
            string name,
            JTokenType type)
        {
            JToken token;
            return jObject.TryGetValue(name, out token) && token.Type == type;
        }
    }

    public class ContentItemConverter : AbstractJsonConverter<ContentItem>
    {
        protected override ContentItem Create(Type objectType, JObject jObject)
        {
            if (FieldExists(jObject, "Content", JTokenType.String))
                return new JournalItem();

            if (FieldExists(jObject, "Text", JTokenType.String))
                return new TextItem();

            if (FieldExists(jObject, "MediaURL", JTokenType.String))
                return new Media();

            throw new InvalidOperationException(); //this means that the json type is unknown, and can't be converted
            //most likely you forgot to add the corresponding type three lines above
        }
    }
}
