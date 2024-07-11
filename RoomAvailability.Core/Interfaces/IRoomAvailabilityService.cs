using RoomAvailability.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAvailability.Core.Interfaces
{
    public interface IRoomAvailabilityService
    {
        Task<bool> IsAvailableForDay(string weekDay, TimeOnly timeSlot, int minuteDuration, CancellationToken cancellationToken);
        Task<DaySchedule?> GetDaySchedule(string weekDay, CancellationToken cancellationToken);
        Task<RoomWeeklySchedule> GetWeeklySchedule(string roomName, CancellationToken cancellationToken);
    }
}
