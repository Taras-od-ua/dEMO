using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Context;
using Core.Dtos;
using Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<CategoryDto>> GetAllSortedByTitleAsync(CancellationToken cancellationToken);
        Task<CategoryDto> InsertAsync(CategoryDto dto, CancellationToken cancellationToken);

        Task<CategoryDto> UpdateAsync(int id, CategoryDto dto, CancellationToken cancellationToken);
        Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken);
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly CategoriesContext _dbContext;
        private readonly ILogger<CategoriesService> _logger;

        public CategoriesService(CategoriesContext dbContext, ILogger<CategoriesService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllSortedByTitleAsync(CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _dbContext.Categories
                    .AsNoTracking()
                    .OrderBy(x => x.Title)
                    .ToListAsync(cancellationToken);

                return categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Color = x.Color,
                    Title = x.Title,
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetAllSortedByTitleAsync");
                throw;
            }
           
        }

        public async Task<CategoryDto> InsertAsync(CategoryDto dto, CancellationToken cancellationToken)
        {


            try
            {
                var cat = new Core.Models.Category
                {
                    Title = dto.Title,
                    Color = dto.Color,
                    is_deleted = false,
                    created_at = DateTime.UtcNow,

                };

                await _dbContext.Categories.AddAsync(cat, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return cat.MapToDto();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "InsertAsync");
                throw;
            }
            
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryDto dto, CancellationToken cancellationToken)
        {
            var car = await _dbContext.Categories
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (car is null)
            {
                return null;
            }

            car.Title = dto.Title;
            car.Color = dto.Color;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return car.MapToDto();
        }

        public async Task<CategoryDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var ent = await _dbContext.Categories.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (ent == null)
            {
                return null;
            }

            return ent.MapToDto();
        }

        public async Task<bool> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var emp = await _dbContext.Categories
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (emp == null)
            {
                return false;
            }

            _dbContext.Categories.Remove(emp);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
