using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class AssetsWithLastLiveDto
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string AssetSerial { get; set; }
    public string AssetType { get; set; } = "GenericAsset";

    public AssetLiveStatusDto LastLiveStatus { get; set; }
}