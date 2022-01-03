using System.Net;

namespace nstu_timetable.Utilities;

public class HtmlLoader
{
    readonly HttpClient client;
    readonly string url;

    public HtmlLoader(string url)
    {
        client = new HttpClient();
        this.url = url;
    }

    public async Task<string?> GetSource()
    {
        var response = await client.GetAsync(url);
        string? source = null;

        if (response is { StatusCode: HttpStatusCode.OK })
        {
            source = await response.Content.ReadAsStringAsync();
        }

        return source;
    }
}