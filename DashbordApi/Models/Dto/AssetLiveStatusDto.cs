using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class AssetLiveStatusDto
{
    public int AssetId { get; set; }
    public string AssetType { get; set; }
  
    public string RawValue { get; set; }
    public string ProcessedStatus { get; set; }
    
    public bool IsVerified { get; set; }

    public DateTime LastUpdate { get; set; }

}