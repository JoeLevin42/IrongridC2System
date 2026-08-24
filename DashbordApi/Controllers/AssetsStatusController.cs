using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DashbordApi.Controllers;

[ApiController]
[Route("assets-status")]
public class AssetsStatusController : ControllerBase
{
    private readonly AssetsStatusRepository _repo;
    public AssetsStatusController(AssetsStatusRepository repo)
    {
        _repo = repo;
    }
    [HttpGet] //Todo check this!!
    public async Task<ActionResult<IEnumerable<AssetLiveStatus>>> GetAllAysnc()
    {
        var all = await _repo.GetAllAssetLiveAsync();
        return Ok(all);
    }

    [HttpGet("({id})")] //this need to be reddis!!!!!!!!!
    public async Task<ActionResult<AssetLiveStatusDto>> GetAssetsLiveByIdAsync(int id)
    {
        var result = await _repo.GetAssetsLiveByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<AssetsWithLastLiveDto?>>> GetAssetsLivesByStatusAsync([FromQuery]string status)
    {
        var result = await _repo.GetAssetsLivesByStatusAsync(status);

        return Ok(result);
    }



}