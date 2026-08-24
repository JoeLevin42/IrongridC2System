using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class CriticalAssetsDto
{
    public int AssetId { get; set; }
    public string AssetSerial { get; set; }

    public string AssetType { get; set; }
    public string UnitName { get; set; }

    public string Sector { get; set; }
    public string ProcessedStatus { get; set; }
    public bool IsVerified { get; set; }

    public DateTime LastUpdate { get; set; }

}

           