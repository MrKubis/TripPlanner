using backend.Application.DTOs;
using backend.Domain.Entities;

namespace backend.Application.Mappers;

public static class ExpenseMapper
{
   public static ExpenseDto ToDto(this Expense expense)
   {
      return new ExpenseDto
      {
         Name = expense.Name,
         Description = expense.Description,
         Price = expense.Price.ToString()
      };
   } 
}