using RoomAvailability.API.Dto;

namespace RoomAvailability.API.Response
{
    public class GetRoomAvailabilityByWeekResponse
    {
        public required string RoomName { get; set; }
        public required IEnumerable<DayScheduleDto> DailySchedules { get; set; }
    }
}
