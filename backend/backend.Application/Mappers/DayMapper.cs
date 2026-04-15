using backend.Application.DTOs;
using backend.Domain.Entities;

namespace backend.Application.Mappers;

public static class DayMapper
{
   public static Day ToEntity(this CreateDayDto dto)
   {
      return new Day
      {
         Date = dto.Date,
         DestinationIds = []
      };
   }

   public static Day ToEntity(this UpdateDayDto dto, string dayId)
   {
      return new Day
      {
         Id = dayId,
         Date = dto.Date,
         DestinationIds = dto.DestinationIds
      };
   }
   public static DayDto ToDto(this Day day)
   {
      return new DayDto
      {
         Id = day.Id,
         Date = day.Date,
         DestinationIds = day.DestinationIds
      };
   }
}