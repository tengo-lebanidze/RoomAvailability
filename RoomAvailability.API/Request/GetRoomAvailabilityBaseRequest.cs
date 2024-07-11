namespace RoomAvailability.API.Request
{
    public record GetRoomAvailabilityBaseRequest
    {
        public required string RoomName { get; init; }
    }
}
