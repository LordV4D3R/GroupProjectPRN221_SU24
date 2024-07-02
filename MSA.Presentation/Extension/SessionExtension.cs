using System.Text;
using System.Text.Json;

namespace MSA.Presentation.Extensions
{
    public static class SessionExtension
        {
            public static void SetObject(this ISession session, string key, object value)
            {
                session.SetString(key, JsonSerializer.Serialize(value));
            }

            public static T GetObject<T>(this ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
            }
        }
}
