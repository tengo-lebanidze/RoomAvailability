using AutoMapper;
using RoomAvailability.API.Dto;
using RoomAvailability.API.Response;
using RoomAvailability.Core.Model;

namespace RoomAvailability.API.MappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DaySchedule, DayScheduleDto>();
            CreateMap<RoomWeeklySchedule, GetRoomAvailabilityByWeekResponse>();
        }
    }
}
