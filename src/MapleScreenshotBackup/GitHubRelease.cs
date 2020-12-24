using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MapleScreenshotBackup
{
    public static class GitHubRelease
    {
        public class ReleaseData
        {
            [JsonPropertyName("html_url")]
            public string Url { get; set; }

            [JsonPropertyName("tag_name")]
            public string TagName { get; set; }
        }

        private const string LatestReleaseLink = "https://api.github.com/repos/pid011/maple-screenshot-backup/releases/latest";
        private static readonly HttpClient s_client = new HttpClient();

        public static async Task<(bool compare, string url)?> CompareVersionAsync(string target, CancellationToken cancellationToken = default)
        {
            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var versionString = $"v{target}";
            var data = await GetLastReleaseAsync(cancellationToken);
            if (data is null)
            {
                return null;
            }
            return (versionString == data.TagName, data.Url);
        }

        private static async Task<ReleaseData> GetLastReleaseAsync(CancellationToken cancellationToken = default)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, LatestReleaseLink);
            request.Headers.Add("User-Agent", "maple-screenshot-backup");
            using var response = await s_client.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            using var jsonStream = await response.Content.ReadAsStreamAsync(cancellationToken);
            return await JsonSerializer.DeserializeAsync<ReleaseData>(jsonStream, cancellationToken: cancellationToken);
        }
    }
}
