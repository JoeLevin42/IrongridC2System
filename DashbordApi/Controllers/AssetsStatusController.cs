using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.Storage;

namespace DashbordApi.Controllers;

[ApiController]
[Route("assets-status")]
public class AssetsStatusController : ControllerBase
{
    private readonly AssetsStatusRepository _repo;
    private readonly HttpClient _client;
    private readonly IDatabase _redis;
    public AssetsStatusController(AssetsStatusRepository repo ,)
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