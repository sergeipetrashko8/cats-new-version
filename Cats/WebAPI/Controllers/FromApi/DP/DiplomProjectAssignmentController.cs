﻿using Application.Core;
using Application.Infrastructure.DPManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.FromApi.DP
{
    public class DiplomProjectAssignmentController : ApiRoutedController
    {
        private readonly LazyDependency<IDpManagementService> _dpManagementService =
            new LazyDependency<IDpManagementService>();

        private IDpManagementService DpManagementService => _dpManagementService.Value;

        [HttpPost]
        public IActionResult Post([FromBody] AssignProjectUpdateModel updateModel)
        {
            DpManagementService.AssignProject( /*todo #auth WebSecurity.CurrentUserId*/1, updateModel.ProjectId,
                updateModel.StudentId);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            DpManagementService.DeleteAssignment( /*todo #auth WebSecurity.CurrentUserId*/1, id);

            return Ok();
        }

        public class AssignProjectUpdateModel
        {
            public int ProjectId { get; set; }

            public int StudentId { get; set; }
        }
    }
}