using Application.Core;
using Application.Infrastructure.CPManagement;
using LMP.Models.CP;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.FromApi.CP
{
    public class CpTaskSheetTemplateController : ApiRoutedController
    {
        private readonly LazyDependency<ICPManagementService> courseProjectManagementService =
            new LazyDependency<ICPManagementService>();

        private ICPManagementService CpManagementService => courseProjectManagementService.Value;

        [HttpGet("{templateId:int}")]
        public IActionResult Get(int templateId)
        {
            var result = CpManagementService.GetTaskSheetTemplate(templateId);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CourseProjectTaskSheetTemplate template)
        {
            template.LecturerId = /*todo #auth WebSecurity.CurrentUserId*/1;
            CpManagementService.SaveTaskSheetTemplate(template);
            return Ok();
        }
    }
}