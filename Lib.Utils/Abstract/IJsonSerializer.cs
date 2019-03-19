using System;

namespace Lib.Utils.Abstract
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string jsonString);
        string Serialize<T>(T obj);
    }
}
