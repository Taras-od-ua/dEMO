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
    public interface IDashboardService
    {
        Task<IEnumerable<CategoryDto>> GetCategoryItems(int id, CancellationToken cancellationToken);
    }

    public class DashboardService : IDashboardService
    {
        private readonly CategoriesContext _dbContext;
        private readonly ILogger<CategoriesService> _logger;

        public DashboardService(CategoriesContext dbContext, ILogger<CategoriesService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        public async Task<IEnumerable<CategoryDto>> GetCategoryItems(int id, CancellationToken cancellationToken)
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
    }
}
