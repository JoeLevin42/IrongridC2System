using DashbordApi.Models;
using System.ComponentModel.DataAnnotations;

namespace DashbordApi.Models;

public class Units
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string UnitName { get; set; } = "Unknown Unit";
    public string Sector { get; set; } =  "General";

    //Np
    public ICollection<Assets> Assets { get; set; } = new List<Assets>();
}