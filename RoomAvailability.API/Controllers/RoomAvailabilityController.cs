using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoomAvailability.API.Dto;
using RoomAvailability.API.Request;
using RoomAvailability.API.Response;
using RoomAvailability.Core.Interfaces;

namespace RoomAvailability.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAvailabilityController : ControllerBase
    {
        private readonly IRoomAvailabilityService _roomAvailabilityService;
        private readonly IMapper _mapper;
        public RoomAvailabilityController(IRoomAvailabilityService roomAvailabilityService, IMapper mapper)
        {
            _roomAvailabilityService = roomAvailabilityService;
            _mapper = mapper;
        }

        [HttpGet("weekly")]
        [ProducesResponseType(typeof(GetRoomAvailabilityByWeekResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoomAvailabilityByWeek([FromQuery] GetRoomAvailabilityByWeekRequest request, CancellationToken cancellationToken)
        {
            var result = await _roomAvailabilityService.GetWeeklySchedule(request.RoomName, cancellationToken);
            if(result == null)
            {
                return BadRequest("Schedule Not Found");
            }

            return Ok(_mapper.Map<GetRoomAvailabilityByWeekResponse>(result));
        }

        [HttpGet("daily")]
        [ProducesResponseType(typeof(GetRoomAvailabilityByDayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoomAvailabilityByDay([FromQuery] GetRoomAvailabilityByDayRequest request, CancellationToken cancellationToken)
        {
            var result = await _roomAvailabilityService.GetDaySchedule(request.WeekDay, cancellationToken);
            if (result == null)
            {
                return BadRequest("Schedule Not Found");
            }

            return Ok(new GetRoomAvailabilityByDayResponse { DaySchedule = _mapper.Map<DayScheduleDto>(result)});

        }

        [HttpGet("time")]
        [ProducesResponseType(typeof(GetRoomAvailabilityByTimeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRoomAvailabilityByTime([FromQuery] GetRoomAvailabilityByTimeRequest request, CancellationToken cancellationToken)
        {
            var result = await _roomAvailabilityService.IsAvailableForDay(request.WeekDay, new TimeOnly(request.Hour, request.Minute), request.MinuteDuration, cancellationToken);

            return Ok(new GetRoomAvailabilityByTimeResponse { Available = result});
        }
    }
}
