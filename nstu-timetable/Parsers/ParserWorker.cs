using AngleSharp.Html.Parser;
using nstu_timetable.Utilities;

namespace nstu_timetable.Parsers;

public class ParserWorker<T> where T : class
{
    private readonly IParser<T> parser;
    private readonly HtmlLoader htmlLoader;

    public ParserWorker(IParser<T> parser, string url)
    {
        this.parser = parser;
        htmlLoader = new HtmlLoader(url);
    }

    public async Task<T> Start()
    {
        var responseString = await htmlLoader.GetSource();
        var htmlParser = new HtmlParser();
        var document = await htmlParser.ParseDocumentAsync(responseString);
        return parser.Parse(document);
    }
}