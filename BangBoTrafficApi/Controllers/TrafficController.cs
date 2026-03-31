using Microsoft.AspNetCore.Mvc;
using BangBoTrafficApi.Models;
using BangBoTrafficApi.Services;

namespace BangBoTrafficApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrafficController : ControllerBase
    {
        private readonly IPlcService _plcService;

        public TrafficController(IPlcService plcService)
        {
            _plcService = plcService;
        }

        [HttpPost("update")]
        public IActionResult UpdateTraffic([FromBody] TrafficRequest request)
        {
            if (request == null) return BadRequest();

            _plcService.SendSignal(request.Lane, request.Status);

            return Ok(new
            {
                Status = "Success",
                Timestamp = DateTime.Now
            });
        }
    }
}