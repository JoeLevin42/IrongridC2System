
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class CreateUnitDto
{
    public string UnitName { get; set; } = "Unknown Unit";
    public string Sector { get; set; } = "General";


}