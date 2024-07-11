namespace RoomAvailability.API.Request
{
    public record GetRoomAvailabilityByDayRequest : GetRoomAvailabilityBaseRequest
    {
        public required string WeekDay { get; init; }
    }
}
