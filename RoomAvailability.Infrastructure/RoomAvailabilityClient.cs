using RoomAvailability.Core.Interfaces.Infrastructure;
using RoomAvailability.Core.Model;
using RoomAvailability.Infrastructure.Response;
using System.Net.Http.Json;
using System.Text.Json;

namespace RoomAvailability.Infrastructure
{
    public class RoomAvailabilityClient : IRoomAvailabilityClient
    {
        private readonly HttpClient _roomAvailabilityClient;
        public RoomAvailabilityClient(HttpClient roomAvailabilityClient)
        {
            _roomAvailabilityClient = roomAvailabilityClient;
        }

        public async Task<IEnumerable<DaySchedule>> GetWeeklySchedule(CancellationToken cancellationToken)
        {
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            jsonOptions.AllowTrailingCommas = true;
            //jsonOptions.PropertyNameCaseInsensitive = true;
            var response = await _roomAvailabilityClient.GetFromJsonAsync<WeeklyScheduleResponse>(string.Empty, jsonOptions, cancellationToken);
            var result = new List<DaySchedule>();
            if (response != null)
            {
                foreach (var item in response.Availability)
                {
                    result.Add(new DaySchedule
                    {
                        WeekDay = item.Key,
                        Availability = item.Value
                            .Select((value, index) => new KeyValuePair<char, int>(value, index))
                            .ToDictionary(k => (new TimeOnly(0).AddMinutes(30 * k.Value)), v => v.Key == '0')
                    });
                }
            }

            return result;
        }
    }
}
