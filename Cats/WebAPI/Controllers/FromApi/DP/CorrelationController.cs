using Application.Core;
using Application.Infrastructure.DPManagement;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.FromApi.DP
{
    public class CorrelationController : ApiRoutedController
    {
        private readonly LazyDependency<ICorrelationService> _correlationService =
            new LazyDependency<ICorrelationService>();

        private ICorrelationService CorrelationService => _correlationService.Value;

        // GET api/<controller>
        [HttpGet]
        public IActionResult Get(string entity)
        {
            return Ok(CorrelationService.GetCorrelation(entity, /*todo #auth WebSecurity.CurrentUserId*/1));
        }
    }
}