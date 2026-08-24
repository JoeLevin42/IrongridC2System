
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashbordApi.Repositories;

public class AssetsStatusRepository
{
    private readonly ApplicationDbContext _context;
    public AssetsStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AssetLiveStatusDto>> GetAllAssetLiveAsync()
    {
        var dtoResultList = _context.AssetLiveStatus
            .Select(e=> new AssetLiveStatusDto
            {
                AssetId = e.AssetId,
                AssetType = e.AssetType,
                RawValue = e.RawValue,
                ProcessedStatus = e.ProcessedStatus,
                IsVerified = e.IsVerified,
                LastUpdate = e.LastUpdate,
            }).ToListAsync();

        return await dtoResultList;
    }

    public async Task<AssetLiveStatusDto?> GetAssetsLiveByIdAsync(int id)
    {
        var theAssetes = await _context.AssetLiveStatus.FirstOrDefaultAsync(e => e.AssetId == id);

        if (theAssetes == null)
        {
            return null;
        }

        var theAssetesDto = new AssetLiveStatusDto
        {
            AssetId = theAssetes.AssetId,
            AssetType = theAssetes.AssetType,
            RawValue = theAssetes.RawValue,
            ProcessedStatus = theAssetes.ProcessedStatus,
            IsVerified = theAssetes.IsVerified,
            LastUpdate = theAssetes.LastUpdate,
        };

        return theAssetesDto;
    }

    public async Task<IEnumerable<AssetsWithLastLiveDto?>> GetAssetsLivesByStatusAsync(string status)
    {
        var result = await _context.AssetLiveStatus.Where(e => e.ProcessedStatus == status)
            .Select(e => new AssetsWithLastLiveDto
            {
                Id = e.Assets.Id,
                UnitId = e.Assets.UnitId,
                AssetSerial = e.Assets.AssetSerial,
                AssetType = e.Assets.AssetType,
                LastLiveStatus = new AssetLiveStatusDto 
                {

                    AssetId = e.AssetId,
                    AssetType = e.AssetType,
                    RawValue = e.RawValue,
                    ProcessedStatus = e.ProcessedStatus,
                    IsVerified = e.IsVerified,
                    LastUpdate = e.LastUpdate,
                }

            }).ToListAsync();

        return result;







    }



}