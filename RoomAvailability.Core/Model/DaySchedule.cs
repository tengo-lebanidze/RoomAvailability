using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomAvailability.Core.Model
{
    public class DaySchedule
    {
        public string WeekDay { get; set; } = string.Empty;
        public Dictionary<TimeOnly, bool> Availability { get; set; } = new Dictionary<TimeOnly, bool>();
    }
}
