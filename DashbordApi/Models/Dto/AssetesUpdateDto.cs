
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class AssetesUpdateDto
{   
    //CAnt update id here.
    public int UnitId { get; set; }
    
    public string AssetSerial { get; set; }
    public string AssetType { get; set; } = "GenericAsset";

   
}