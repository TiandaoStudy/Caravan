namespace Finsa.Caravan.Common
{
    public interface IJsonSerializer
    {
        string SerializeObject<TObj>(TObj obj);

        TObj DeserializeObject<TObj>(string json);
    }
}