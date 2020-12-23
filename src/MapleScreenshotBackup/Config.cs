using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MapleScreenshotBackup
{
    public record BackupDirectories
    {
        [JsonPropertyName("maplestory")]
        public string MapleDirectory { get; init; }
        [JsonPropertyName("backup")]
        public string BackupDirectory { get; init; }
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
        public static async Task<BackupDirectories> LoadConfig()
        {
            try
            {
                using var fs = File.OpenRead(FilePath);
                var obj = await JsonSerializer.DeserializeAsync<BackupDirectories>(fs);

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<bool> WriteConfig(BackupDirectories obj)
        {
            try
            {
                // TODO: 저장할때 한글이 제대로 저장되도록 수정하기
                using var fs = File.Open(FilePath, FileMode.Create);
                await JsonSerializer.SerializeAsync(fs, obj, options: s_option);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
