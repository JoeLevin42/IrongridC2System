using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class SummarizeUnitsDto
{
    public int UnitId { get; set; }
    public string UnitName { get; set; }

    public string Sector { get; set; }
    public int TotalAssets { get; set; }
    public int StableAssets { get; set; }
    public int WarningAssets { get; set; }
    public int UnverifiedAssets { get; set; }
}


