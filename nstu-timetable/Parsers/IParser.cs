using AngleSharp.Html.Dom;

namespace nstu_timetable.Parsers;

public interface IParser<out T> where T : class
{
    T Parse(IHtmlDocument document);
}