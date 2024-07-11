using RoomAvailability.Core.Model;

namespace RoomAvailability.Core.Interfaces.Infrastructure
{
    public interface IRoomAvailabilityClient
    {
        Task<IEnumerable<DaySchedule>> GetWeeklySchedule(CancellationToken cancellationToken);
    }
}
