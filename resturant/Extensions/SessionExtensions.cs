using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace resturant.Extensions
{
    public static class SessionExtensions
    {
        // حفظ أي كائن أو قائمة داخل السيشن بعد تحويلها لنص
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        // استرجاع الكائن أو القائمة من السيشن وتحويلها لنوعها الأصلي
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}