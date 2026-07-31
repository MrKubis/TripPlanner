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
      };
   }

   public static Day ToEntity(this UpdateDayDto dto, Guid dayId)
   {
      return new Day
      {
         Id = dayId,
         Date = dto.Date,
      };
   }
   public static DayDto ToDto(this Day day)
   {
      return new DayDto
      {
         Id = day.Id,
         Date = day.Date,
      };
   }
}