using System.IO;

namespace BVT.RepositoryWebApi
{
    public interface ISerialization
    {
        string Serialize(object o);
        object DeSerialize(Stream stream);
    }
}
