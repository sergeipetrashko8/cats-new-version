using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///     <see cref="ControllerBase"/> class with [<see cref="ApiControllerAttribute"/>] and [<see cref="RouteAttribute"/>("api/[controller]")].
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ApiRoutedController : ControllerBase
    {
        public ObjectResult ServerError500<T>(T data)
        {
            var result = StatusCode((int) HttpStatusCode.InternalServerError, data);

            return result;
        }
    }
}