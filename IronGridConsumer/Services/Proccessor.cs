

using IronGridConsumer.Data;
using IronGridConsumer.Models;
using Microsoft.EntityFrameworkCore;
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
    {   //need to check if the assests id exists for not getting constaint violation
        try
        {
            var assestLiveObj = JsonSerializer.Deserialize<AssetLiveStatus>(json);
            if (assestLiveObj == null)
            {
                return false;
            }

            //need to check that the assets hitseld alive

            var checkAssets = await _context.Assets.AsNoTracking()
                .AnyAsync(e => e.Id == assestLiveObj.AssetId);
            if (checkAssets == false)
            {
                return false;
            }

            //logic rules here hand check if exists nad remove ?? await
            var exists = await _context.AssetLiveStatus
                .FirstOrDefaultAsync(e => e.AssetId == assestLiveObj.AssetId);
            if (exists != null)
            {
                _context.AssetLiveStatus.Remove(exists);
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