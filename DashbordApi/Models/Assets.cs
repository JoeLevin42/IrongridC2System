using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class Assets
{
    [Required]
    [Key]
    public int Id { get; set; }
    public int UnitId { get; set; }
    [Required]
    public string AssetSerial { get; set; }
    public string AssetType { get; set; } = "GenericAsset";

    //Np
    public Units Units { get; set; } = null!;

    public AssetLiveStatus AssetLiveStatus { get; set; } = null!;
    
}