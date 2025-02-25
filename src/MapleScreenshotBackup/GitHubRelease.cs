using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MapleScreenshotBackup;

public static class GitHubRelease
{
    private const string LatestReleaseLink =
        "https://api.github.com/repos/pid011/maple-screenshot-backup/releases/latest";

    private static readonly HttpClient s_client = new();

    /// <summary>
    ///     Compare version
    /// </summary>
    /// <param name="target">Version string to compare. e.g. [1.0.0]</param>
    /// <param name="cancellationToken">The cancellation token to cancel operation.</param>
    /// <returns>
    ///     Return null if cannot find github release. Or return true if target version is same or greater than github
    ///     release version.
    /// </returns>
    public static async Task<(bool compare, ReleaseData release)?> CompareVersionAsync(string target,
        CancellationToken cancellationToken = default)
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

        return (versionString.CompareTo(data.TagName) >= 0, data);
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

        await using var jsonStream = await response.Content.ReadAsStreamAsync(cancellationToken);
        return await JsonSerializer.DeserializeAsync<ReleaseData>(jsonStream, cancellationToken: cancellationToken);
    }

    public class ReleaseData
    {
        [JsonPropertyName("html_url")] public string Url { get; set; }

        [JsonPropertyName("tag_name")] public string TagName { get; set; }
    }
}
