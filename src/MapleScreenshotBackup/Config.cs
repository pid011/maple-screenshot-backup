using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapleScreenshotBackup
{
    public class ConfigItem
    {
        [JsonPropertyName("screenshot")]
        public string ScreenshotDirectory { get; set; }

        [JsonPropertyName("backup")]
        public string BackupDirectory { get; set; }

        [JsonPropertyName("can_delete")]
        public bool CanDelete { get; set; }
    }

    public static partial class Config
    {
        public const string FileName = "config.json";

        public static ConfigItem Item { get; private set; }

        private static string FilePath => Path.Combine(AppContext.BaseDirectory, FileName);

        private static readonly JsonSerializerOptions s_option = new JsonSerializerOptions { WriteIndented = true };
        private static readonly AsyncLock s_lock = new AsyncLock();

        /// <summary>
        /// Load config from file.
        /// </summary>
        /// <returns>Return null if config file does not exist or json contents are broken.</returns>
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
                item = new ConfigItem
                {
                    ScreenshotDirectory = string.Empty,
                    BackupDirectory = string.Empty,
                    CanDelete = true
                };
                await SaveAsync();
            }

            Item = item;
        }

        public static async Task SaveAsync()
        {
            // TODO: 저장할때 한글이 제대로 저장되도록 수정하기
            using (await s_lock.LockAsync())
            {
                using var fs = File.Open(FilePath, FileMode.Create);
                await JsonSerializer.SerializeAsync(fs, Item, options: s_option);
            }
        }
    }
}
