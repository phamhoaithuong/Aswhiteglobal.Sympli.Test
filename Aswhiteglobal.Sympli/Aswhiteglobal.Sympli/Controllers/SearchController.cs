using Microsoft.AspNetCore.Mvc;
using Sympli.Core.Interfaces;
using Sympli.Core.Models;

namespace Aswhiteglobal.Sympli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost()]
        public async Task<IActionResult> PostAsync([FromBody] QuerySEO search, CancellationToken token = default)
        {
            var result = await _searchService.QuerySEOAsync(search, token);
            return Ok(result);
        }
    }
}