using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using StackExchange.Redis;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using RedisResult = DashbordApi.Models.RedisResult;

namespace DashbordApi.Controllers;

[ApiController]
[Route("assets-status")]
public class AssetsStatusController : ControllerBase
{
    private readonly HttpClient _client;
    private readonly IDatabase _redis;

    private readonly AssetsStatusRepository _repo;
    public AssetsStatusController(AssetsStatusRepository repo,
        HttpClient client, IConnectionMultiplexer muxer)
    {
        _repo = repo;
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("weatherCachingApp", "1.0"));
        _redis = muxer.GetDatabase();
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
        var watch = Stopwatch.StartNew();
        var key = $"AssetLiveStatusDto:{id}";
        var cached = await _redis.StringGetAsync(key);
        if (cached.HasValue)
        {
            var chaceResult = JsonSerializer.Deserialize<AssetLiveStatus>(cached);
            Console.WriteLine("Reddis is working!!!!!!");
            return Ok(chaceResult);
        }
        
        var result = await _repo.GetAssetsLiveByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        var jsonResult = JsonSerializer.Serialize(result);
        var setTask = _redis.StringSetAsync(key, jsonResult);
        var expireTask = _redis.KeyExpireAsync(key, TimeSpan.FromMinutes(5));
        await Task.WhenAll(setTask, expireTask);


        watch.Stop();
        var res = new RedisResult(result, watch.ElapsedMilliseconds);
        return Ok(result);
    }

    [HttpGet("status")]
    public async Task<ActionResult<IEnumerable<AssetsWithLastLiveDto?>>> GetAssetsLivesByStatusAsync([FromQuery]string status)
    {
        var result = await _repo.GetAssetsLivesByStatusAsync(status);

        return Ok(result);
    }



}