using System.Linq;
using Core.Dtos;
using Core.Models;

namespace Core.Extensions
{
    internal static class Extensions
    {
        public static EmployeeDto MapToDto(this Employee source)
        {
            return new EmployeeDto
            {
                Id = source.EmpNo,
                FirstName = source.FirstName,
                LastName = source.LastName,
                BirthDate = source.BirthDate,
                Gender = source.Gender,
            };
        }

        public static CategoryDto MapToDto(this Category source)
        {
            return new CategoryDto()
            {
                Id = source.Id,
                Title = source.Title,
                Color = source.Color,
                Lists = source.Lists.Select(l=>l.MapToDto())
            };
        }

        public static ListDto MapToDto(this List source)
        {
            return new ListDto()
            {
                Id = source.Id,
                Title = source.Title,
                CategoryId = source.CategoryId,
                Items = source.Items.Select(l => l.MapToDto())
            };
        }

        public static ItemDto MapToDto(this Item source)
        {
            return new ItemDto()
            {
                Id = source.Id,
                Title = source.Title,
                Done = source.Done,
                ListId = source.ListId,
                
            };
        }
    }
}
