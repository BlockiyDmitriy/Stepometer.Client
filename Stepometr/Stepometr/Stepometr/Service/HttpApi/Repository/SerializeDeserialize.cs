using Newtonsoft.Json;
using System.Collections.Generic;

namespace Stepometer.Service.HttpApi.Repository
{
    public class SerializeDeserialize<TModel> where TModel : class
    {
        public static string ConvertToJson(TModel self) =>
            JsonConvert.SerializeObject(self);

        public static List<TModel> ConvertFromJson(string json) =>
            JsonConvert.DeserializeObject<List<TModel>>(json);
    }
}