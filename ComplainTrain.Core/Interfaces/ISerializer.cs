namespace ComplainTrain.Core.Interfaces
{
    public interface ISerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string obj);
    }
}