namespace RoomAvailability.Core.Model
{
    public class RoomWeeklySchedule
    {
        public required string RoomName { get; set; }
        public required IEnumerable<DaySchedule> DailySchedules { get; set; }
    }
}
