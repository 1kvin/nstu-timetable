using Microsoft.VisualStudio.TestTools.UnitTesting;
using nstu_timetable.Parsers;
using System.IO;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;

namespace nstu_timetable_tests.ParseTests
{
    [TestClass]
    public class GroupParseTest
    {
        [TestMethod]
        public async Task GoodSchedulePageTest()
        {
            var parser = new GroupParser();
            var htmlParser = new HtmlParser();
            var document = await htmlParser.ParseDocumentAsync(await File.ReadAllTextAsync("HtmlExample/SchedulePage/GoodSchedulePage.html"));
            var result =  parser.Parse(document);
            Assert.IsTrue(result.Count == 945);
        }
    }
}
