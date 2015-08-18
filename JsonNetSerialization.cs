using System.IO;
using Newtonsoft.Json;

namespace BVT.RepositoryWebApi
{
    public class JsonNetSerialization : ISerialization
    {
        public string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public object DeSerialize(Stream stream)
        {
            return JsonConvert.DeserializeObject(new StreamReader(stream).ReadToEnd());
        }
    }
}
