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
        public const string LeaderboardFileName = "config.json";
        private static string FilePath => Path.Combine(ProgramDirectoryPath, LeaderboardFileName);
        private static string ProgramDirectoryPath => Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName;

        private readonly static JsonSerializerOptions s_option = new JsonSerializerOptions { WriteIndented = true };

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
