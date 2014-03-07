namespace WGSharpAPI.Interfaces
{
    public interface IWGResponse<T>
    {
        string Status { get; set; }

        int Count { get; set; }

        T Data { get; set; }
    }
}
