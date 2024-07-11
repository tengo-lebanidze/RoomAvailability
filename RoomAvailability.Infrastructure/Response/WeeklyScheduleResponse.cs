namespace RoomAvailability.Infrastructure.Response
{
    internal class WeeklyScheduleResponse
    {
        public Dictionary<string, string> Availability { get; set; } = new Dictionary<string, string>();
    }
}
