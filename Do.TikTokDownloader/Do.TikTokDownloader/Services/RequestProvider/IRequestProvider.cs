using System.Threading.Tasks;

namespace Do.TikTokDownloader.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");
        Task<string> GetUrlAsync(string uri, string token = "");
        Task<string> DownloadByteAsync(string uri, string token = "");
        Task<byte[]> GetByteAsync(string uri, string token = "");
        Task<string> GetHtmlAsync(string uri, string token = "");
        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task DeleteAsync(string uri, string token = "");
    }
}