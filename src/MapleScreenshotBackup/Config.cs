using System;
using System.IO;
using System.Reflection;
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

    public static class Config
    {
        public const string FileName = "config.json";
        public static string FilePath => Path.Combine(ProgramDirectoryPath, FileName);
        private static string ProgramDirectoryPath => Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

        private static readonly JsonSerializerOptions s_option = new JsonSerializerOptions { WriteIndented = true };

        /// <summary>
        /// Load config from file.
        /// </summary>
        /// <returns>Return null if config file does not exist or json contents are broken.</returns>
        public static async Task<ConfigItem> LoadConfig()
        {
            try
            {
                using var fs = File.OpenRead(FilePath);
                var obj = await JsonSerializer.DeserializeAsync<ConfigItem>(fs);

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task WriteConfig(ConfigItem obj)
        {
            // TODO: 저장할때 한글이 제대로 저장되도록 수정하기
            using var fs = File.Open(FilePath, FileMode.Create);
            await JsonSerializer.SerializeAsync(fs, obj, options: s_option);
        }
    }
}
