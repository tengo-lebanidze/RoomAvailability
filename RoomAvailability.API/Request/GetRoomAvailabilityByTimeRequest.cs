using System.ComponentModel.DataAnnotations;

namespace RoomAvailability.API.Request
{
    public record GetRoomAvailabilityByTimeRequest : GetRoomAvailabilityByDayRequest
    {
        [Range(0, 23)]
        public int Hour { get; init; }
        [Range(0, 59)]
        public int Minute { get; init; }
        public int MinuteDuration { get; init; }
    }
}
