using Microsoft.VisualStudio.TestTools.UnitTesting;
using nstu_timetable.Parsers;
using System.IO;
using System.Threading.Tasks;

namespace nstu_timetable_tests.ParseTests
{
    [TestClass]
    public class GroupParseTest
    {
        [TestMethod]
        public async Task GoodSchedulePageTest()
        {
            var result = await GroupParser.ParseAllGroupAsync(await File.ReadAllTextAsync("HtmlExample/SchedulePage/GoodSchedulePage.html"));
            Assert.IsTrue(result.Count == 945);
        }
    }
}
