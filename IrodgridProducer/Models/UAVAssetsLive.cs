using IrodgridProducer.Models;

namespace IrodgridProducer.Models;

public class UAVAssetsLive
{
    public int AssetId { get; set; }
    public string AssetType { get; set; }
    public int RawValue { get; set; }
    public string? ProcessedStatus { get; set; }
    public bool? IsVerified { get; set; }
    public DateTime LastUpdate { get; set; }
}