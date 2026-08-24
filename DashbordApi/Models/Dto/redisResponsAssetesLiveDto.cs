using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class ForecastResult
{
    public long ElapsedTime { get; }
    public AssetLiveStatusDto AssetLiveStatusDto { get; }

    public ForecastResult(AssetLiveStatusDto AssetLiveStatus, long elapsedTime)
    {
        AssetLiveStatusDto = AssetLiveStatus;
        ElapsedTime = elapsedTime;
    }
}
