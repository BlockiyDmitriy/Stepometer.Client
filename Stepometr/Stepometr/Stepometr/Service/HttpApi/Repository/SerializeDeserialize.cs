using Newtonsoft.Json;

namespace Stepometer.Service.HttpApi.Repository
{
    public class SerializeDeserialize<TModel> where TModel : class
    {
        public static string ConvertToJson(TModel self) =>
            JsonConvert.SerializeObject(self);

        public static TModel ConvertFromJson(string json) =>
            JsonConvert.DeserializeObject<TModel>(json);
    }
}