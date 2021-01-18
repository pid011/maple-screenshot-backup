using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapleScreenshotBackup
{
    public enum FinishedScreenshotOption
    {
        SendToRecycleBin,
        DeletePermanently,
        DoNotDelete
    }

    public class ConfigItem
    {
        [JsonPropertyName("screenshot")]
        public string ScreenshotDirectory { get; set; }

        [JsonPropertyName("backup")]
        public string BackupDirectory { get; set; }

        [JsonPropertyName("finished_screenshot_option")]
        public FinishedScreenshotOption FinishedScreenshot { get; set; }
    }

    public static partial class Config
    {
        public const string FileName = "config.json";

        public static ConfigItem Item { get; private set; }

        private static string FilePath => Path.Combine(AppContext.BaseDirectory, FileName);

        private static readonly JsonSerializerOptions s_option = new JsonSerializerOptions { WriteIndented = true };
        private static readonly AsyncLock s_lock = new AsyncLock();

        public static async Task LoadAsync()
        {
            ConfigItem item = null;
            try
            {
                using (await s_lock.LockAsync())
                {
                    using var fs = File.OpenRead(FilePath);
                    item = await JsonSerializer.DeserializeAsync<ConfigItem>(fs);
                }
            }
            catch (Exception)
            {
            }

            if (item is null)
            {
                Item = new ConfigItem
                {
                    ScreenshotDirectory = string.Empty,
                    BackupDirectory = string.Empty,
                    FinishedScreenshot = FinishedScreenshotOption.SendToRecycleBin
                };
                await SaveAsync();
            }
            else
            {
                Item = item;
            }
        }

        public static async Task SaveAsync()
        {
            using var disposer = await s_lock.LockAsync();

            // TODO: 저장할때 한글이 제대로 저장되도록 수정하기
            using var fs = File.Open(FilePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(fs, Item, options: s_option);
        }
    }
}
