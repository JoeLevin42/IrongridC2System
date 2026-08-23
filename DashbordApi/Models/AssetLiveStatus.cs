using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class AssetLiveStatus
{
    [Key]
    public int AssetId { get; set; }

    [RegularExpression("^UAV|PerimeterSensor$")]
    public string AssetType { get; set; }
    [Required]
    public string RawValue { get; set; }

    [RegularExpression("^Stable|Warning$")]
    public string ProcessedStatus { get; set; }
    [Required]
    public bool IsVerified { get; set; }
    [Required]
    public DateTime LastUpdate { get; set; }
    

    //Np 
    public Assets Assets { get; set; } = null!;
}