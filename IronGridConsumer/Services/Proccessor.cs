

using IronGridConsumer.Data;
using IronGridConsumer.Models;
using System.Text.Json;

namespace IronGridConsumer.Services;

public class Proccessor
{
    private readonly ApplicationDbContext _context;
    public Proccessor(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> ProccessAssetLiveStatus(string json)
    {
        try
        {
            var assestLiveObj = JsonSerializer.Deserialize<AssetLiveStatus>(json);
            if (assestLiveObj == null)
            {
                return false;
            }

            //logic rules here hand check if exists nad remove
           var exists = _context.AssetLiveStatus
                .FirstOrDefault(e => e.AssetId == assestLiveObj.AssetId);
            if (exists != null)
            {
                _context.AssetLiveStatus.Remove(exists);
                await _context.SaveChangesAsync();
            } 

            _context.AssetLiveStatus.Add(assestLiveObj);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }

    }
}