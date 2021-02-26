using Microsoft.AspNetCore.Mvc;
using MyLab.ApiClient;
using MyLab.Log.Ctx;
using MyLab.Log.Dsl;

namespace TestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        private readonly IDslLogger _log;

        public EchoController(IDslLogger<EchoController> log)
        {
            _log = log;
        }

        [Get("echo")]
        public IActionResult Echo(string message)
        {
            _log.Action("Echo").Write();

            return Ok(message);
        }
    }
}
