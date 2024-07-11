using RoomAvailability.Core.Interfaces;
using RoomAvailability.Core.Interfaces.Infrastructure;
using RoomAvailability.Core.Model;

namespace RoomAvailability.Core.Services
{
    public class RoomAvailabilityService : IRoomAvailabilityService
    {
        private readonly IRoomAvailabilityClient _roomAvailabilityClient;
        public RoomAvailabilityService(IRoomAvailabilityClient roomAvailabilityClient)
        {
            _roomAvailabilityClient = roomAvailabilityClient;
        }

        public async Task<DaySchedule?> GetDaySchedule(string weekDay, CancellationToken cancellationToken)
        {
            var dailySchedules = await _roomAvailabilityClient.GetWeeklySchedule(cancellationToken);
            var daySchedule = dailySchedules.FirstOrDefault(s =>  s.WeekDay == weekDay);
            return daySchedule;
        }

        public async Task<RoomWeeklySchedule> GetWeeklySchedule(string roomName, CancellationToken cancellationToken)
        {
            var dailySchedules = await _roomAvailabilityClient.GetWeeklySchedule(cancellationToken);
            var result = new RoomWeeklySchedule
            {
                RoomName = roomName,
                DailySchedules = dailySchedules
            };

            return result;
        }

        public async Task<bool> IsAvailableForDay(string weekDay, TimeOnly timeSlot, int minuteDuration, CancellationToken cancellationToken)
        {
            var dailySchedules = await _roomAvailabilityClient.GetWeeklySchedule(cancellationToken);

            var daySchedule = dailySchedules.FirstOrDefault(s => s.WeekDay == weekDay);
            if (daySchedule == null)
            {
                return false;
            }

            var slotCount = minuteDuration / 30 + (minuteDuration % 30 > 0 ? 1 : 0);
            for ( var i = 0; i < slotCount; i++ )
            {
                bool isAvailable = false;
                if(!daySchedule.Availability.TryGetValue(timeSlot.AddMinutes(30 * i), out isAvailable) || !isAvailable)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
