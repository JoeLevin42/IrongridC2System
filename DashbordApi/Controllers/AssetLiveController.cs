using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DashbordApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetLiveController : ControllerBase
{
    private readonly DashbordRepository _repo;
    public AssetLiveController(DashbordRepository repo)
    {
        _repo = repo;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AssetLiveStatus>>> GetAllAysnc()
    {
        var all = await _repo.GetAllAssetLiveAsync();
        return Ok(all);
    }
}