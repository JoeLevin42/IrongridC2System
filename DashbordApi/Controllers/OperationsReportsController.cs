using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DashbordApi.Controllers;

[ApiController]
[Route("reports")]
public class OperationsReportsController : ControllerBase
{
    private readonly OperationsReportsRepository _repo;
    public OperationsReportsController(OperationsReportsRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("critical-assets")]
    public async Task<ActionResult<IEnumerable<CriticalAssetsDto>>> GetAllCriticalsAsync()
    {
        var result = await _repo.GetAllCriticalsAsync();

        return Ok(result);
    }

    [HttpGet("/unit/{unitId}/assets")]
    public async Task<ActionResult<IEnumerable<AssetsStatusDto>>> GetAllAssetsStatusByUnitAsync(int unitId)
    {
        var result = await _repo.GetAllAssetsStatusByUnitAsync(unitId);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    [HttpGet("summary-by-unit")]
    public async Task<ActionResult<IEnumerable<SummarizeUnitsDto>>> SummaryAllUnitsAsync()
    {
        var result = await _repo.SummaryAllUnitsAsync();

        return Ok(result);
    }
}