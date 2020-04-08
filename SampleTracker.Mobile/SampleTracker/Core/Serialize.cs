using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SampleTracker.Core
{
    public static class Serialize
    {
        private static readonly IsoDateTimeConverter dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" };
        private static readonly JsonSerializerSettings jsonBeautify = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
        };
        private static readonly JsonSerializerSettings jsonMinify = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None,
            DateFormatString = "dd/MM/yyyy HH:mm:ss",
        };

        /// <summary>
        /// Serializa um objeto em JSON
        /// </summary>
        /// <typeparam name="T">Tipo do objeto que será serializado</typeparam>
        /// <param name="mObject">Object a ser serializado</param>
        /// <returns></returns>
        public static string ObjectToJson<T>(this T mObject)
        {
            var json = JsonConvert.SerializeObject(mObject);
            return json.ToString();
        }

        /// <summary>
        /// Deserializa um JSON em objeto
        /// </summary>
        /// <typeparam name="T">Tipo do objeto que será deserializado</typeparam>
        /// <param name="json">String que será deserializada em um objeto</param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            T mObject = JsonConvert.DeserializeObject<T>(json);
            return mObject;
        }
    }
}
