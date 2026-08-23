using IronGridConsumer.Models;
using System.ComponentModel.DataAnnotations;

namespace IronGridConsumer.Models;

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