using IronGridConsumer.Models;

namespace IronGridConsumer.Models;

public class PerimeterSensor
{
    public int AssetId { get; set; }
    public string Type { get; set; }
    public string RawValue { get; set; }
    public string ProcessedStatus { get; set; }
    public bool IsVerified { get; set; }
    public DateTime LastUpdate { get; set; }

  
}

