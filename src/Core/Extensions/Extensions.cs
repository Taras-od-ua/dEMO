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
            };
        }
    }
}
