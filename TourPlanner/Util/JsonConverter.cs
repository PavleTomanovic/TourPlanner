using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Util
{
    public class JsonConverter
    {
        public static T ConvertFromJson<T>(string message)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings()
                {
                    DefaultValueHandling = DefaultValueHandling.Populate,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    NullValueHandling = NullValueHandling.Ignore
                };
                JsonConvert.DeserializeObject<T>(message, settings);
                return (T)Convert.ChangeType((object)JsonConvert.DeserializeObject<T>(message, settings), typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while serializing Json: " + ex.Message);
            }

        }
    }
}
