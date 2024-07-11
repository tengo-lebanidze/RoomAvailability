using RoomAvailability.API.Dto;

namespace RoomAvailability.API.Response
{
    public class GetRoomAvailabilityByDayResponse
    {
        public required DayScheduleDto DaySchedule { get; set; }
    }
}
