using System.Threading.Tasks;

namespace Do.TikTokDownloader.Services.Settings
{
    public interface ISettingsService
    {
        bool IsFirstLogin { get; set; }
        bool GetValueOrDefault(string key, bool defaultValue);
        string GetValueOrDefault(string key, string defaultValue);
        Task AddOrUpdateValue(string key, bool value);
        Task AddOrUpdateValue(string key, string value);
    }
}
