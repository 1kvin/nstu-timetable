using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using nstu_timetable.DbContexts;

namespace nstu_timetable.Parsers
{
    public class GroupParser : IParser<List<Group>>
    {
        private const string FACULTY_SELECTOR = "div.schedule__faculty.js-schedule-faculty";
        private const string FACULTY_NAME_SELECTOR = "a.schedule__faculty-title.js-schedule-faculty-back";
        private const string FACULTY_TYPE_SUBTITLE_SELECTOR = "div.schedule__faculty-type__subtitle";
        private const string COURSE_NUMBER_SELECTOR = "label.schedule__faculty-course__title";
        private const string GROUP_SELECTOR = "div.schedule__faculty-groups";

        public List<Group> Parse(IHtmlDocument document)
        {
            var faculties = document.QuerySelectorAll(FACULTY_SELECTOR);

            var result = new List<Group>();

            Parallel.ForEach(faculties, f => result.AddRange(ParseFaculty(f)));

            return result;
        }

        private static IEnumerable<Group> ParseFaculty(IParentNode element)
        {
            var facultyName = element.QuerySelector(FACULTY_NAME_SELECTOR).TextContent.Trim();

            var groups = element.QuerySelectorAll(GROUP_SELECTOR);

            var facultyTypeSubtitles = element.QuerySelectorAll(FACULTY_TYPE_SUBTITLE_SELECTOR)
                .Select(e => e.TextContent).ToList();

            var courseNums = element.QuerySelectorAll(COURSE_NUMBER_SELECTOR)
                .Select(z => z.QuerySelector("span").TextContent).Select(ParseCourse).ToList();


            if (groups.Length != courseNums.Count)
            {
                throw new FormatException("course# length not equals groups length");
            }

            var result = new List<Group>();
            Faculty faculty = null;
            int facultyCounter = 0;

            for (int i = 0; i < groups.Length; i++)
            {
                if (courseNums[i] == 1)
                {
                    faculty = new Faculty()
                    {
                        FullName = facultyName,
                        TypeSubtitle = facultyTypeSubtitles[facultyCounter]
                    };
                    facultyCounter++;
                }

                var groupInCourse = ParseGroupsInCourse(groups[i]);
                groupInCourse.ForEach(g =>
                {
                    g.CourseNumber = courseNums[i];
                    g.Faculty = faculty;
                });
                result.AddRange(groupInCourse);
            }

            return result;
        }

        private static List<Group> ParseGroupsInCourse(IParentNode element)
        {
            var result = element.QuerySelectorAll("a").Select(ParseGroup).ToList();
            result.AddRange(element.QuerySelectorAll("span").Select(ParseNotReadyGroup));
            return result;
        }

        private static Group ParseNotReadyGroup(IElement element)
        {
            return new Group()
            {
                ScheduleUrl = null,
                GroupName = element.TextContent
            };
        }

        private static Group ParseGroup(IElement element)
        {
            var groupName = element.TextContent;
            var scheduleUrl = element.GetAttribute("href");


            return new Group()
            {
                ScheduleUrl = scheduleUrl,
                GroupName = groupName,
            };
        }

        private static int ParseCourse(string courserNumStr)
        {
            return int.Parse(new string(courserNumStr.Where(char.IsDigit).ToArray()));
        }
    }
}