using WGSharpAPI.Interfaces;

namespace WGSharpAPI
{
    public class WGResponse<T> : IWGResponse<T> where T : class, new()
    {
        public string Status { get; set; }

        public int Count { get; set; }

        public T Data { get; set; }
    }
}
