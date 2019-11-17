using Application.Core;
using Application.Infrastructure.DPManagement;
using LMP.Models.DP;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.FromApi.DP
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TaskSheetTemplateController : ApiRoutedController
    {
        private readonly LazyDependency<IDpManagementService> diplomProjectManagementService =
            new LazyDependency<IDpManagementService>();

        private IDpManagementService DpManagementService => diplomProjectManagementService.Value;

        [HttpGet("{templateId:int}")]
        public IActionResult Get(int templateId)
        {
            var result = DpManagementService.GetTaskSheetTemplate(templateId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DiplomProjectTaskSheetTemplate template)
        {
            template.LecturerId = /*todo #auth WebSecurity.CurrentUserId*/2;
            DpManagementService.SaveTaskSheetTemplate(template);
            return Accepted();
        }
    }
}