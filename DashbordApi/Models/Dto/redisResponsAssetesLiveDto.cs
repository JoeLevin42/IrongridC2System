using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class RedisResult
{
    public long ElapsedTime { get; }
    public AssetsWithLastLiveDto AssetLiveStatusDto { get; }

    public RedisResult(AssetsWithLastLiveDto AssetLiveStatus, long elapsedTime)
    {
        AssetLiveStatusDto = AssetLiveStatus;
        ElapsedTime = elapsedTime;
    }
}
