using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Lib.Utils.Abstract;
using System;

namespace Lib.Utils.Concrete
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
