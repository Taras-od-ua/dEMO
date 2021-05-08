using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Services;

namespace Api.Controllers
{
    [Route("api/categories")]
    public class DashboardController : ApiControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryItems(int id, CancellationToken cancellationToken = default)
        {
            var result = await _dashboardService.GetCategoryItems(id, cancellationToken);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        
       /* [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] CategoryDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _categoriesService.InsertAsync(dto, cancellationToken);
            Response.Headers.Add("x-date-created", DateTime.UtcNow.ToString("s"));
            return CreatedAtAction("Get", new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CategoryDto dto, CancellationToken cancellationToken = default)
        {
            var result = await _categoriesService.UpdateAsync(id, dto, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = await _categoriesService.DeleteByIdAsync(id, cancellationToken);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }*/


    }
}
