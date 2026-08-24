using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class RedisResult
{
    public long ElapsedTime { get; }
    public AssetLiveStatusDto AssetLiveStatusDto { get; }

    public RedisResult(AssetLiveStatusDto AssetLiveStatus, long elapsedTime)
    {
        AssetLiveStatusDto = AssetLiveStatus;
        ElapsedTime = elapsedTime;
    }
}
