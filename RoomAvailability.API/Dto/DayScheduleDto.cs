namespace RoomAvailability.API.Dto
{
    public class DayScheduleDto
    {
        public string WeekDay { get; set; } = string.Empty;
        public Dictionary<TimeOnly, bool> Availability { get; set; } = new Dictionary<TimeOnly, bool>();
    }
}
