using Newtonsoft.Json;

namespace Nexto.Web.Helpers
{
    public static class Serializer
    {
        public static T GetObject<T>(string json)
        {
            if (!string.IsNullOrWhiteSpace(json))
                return JsonConvert.DeserializeObject<T>(json);
            return default(T);
        }

        public static string SetObject<T>(T objeto)
        {
            if (objeto != null)
                return JsonConvert.SerializeObject(objeto);
            return null;
        }
    }
}
