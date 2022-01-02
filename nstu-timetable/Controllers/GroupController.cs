using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using nstu_timetable.DbContexts;
using nstu_timetable.Services;

namespace nstu_timetable.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly NstuTimetableContext nstuTimetableContext;
        private readonly GroupSyncService groupSyncService;

        public GroupController(NstuTimetableContext nstuTimetableContext, GroupSyncService groupSyncService)
        {
            this.nstuTimetableContext = nstuTimetableContext;
            this.groupSyncService = groupSyncService;
        }

        [HttpGet]
        [Route("startSync")]
        public async Task<string> StartSync()
        {
            await groupSyncService.StartSyncAsync();

            return "OK";
        }

        [HttpGet]
        [Route("getAll")]
        public List<Group> GetAll()
        {
            return nstuTimetableContext.Groups.ToList();
        }
    }
}
