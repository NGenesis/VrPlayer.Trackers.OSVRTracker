namespace VrPlayer.Helpers.Serialization
{
    public interface ISerializer<T>
    {
        string Serialize(T obj); 
        T Deserialize(string data);
    }
}
