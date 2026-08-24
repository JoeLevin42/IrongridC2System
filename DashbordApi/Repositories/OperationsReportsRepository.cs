
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashbordApi.Repositories;

public class OperationsReportsRepository
{
    private readonly ApplicationDbContext _context;
    public OperationsReportsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CriticalAssetsDto>> GetAllCriticalsAsync()
    {
        var result = _context.AssetLiveStatus
            .Where(e => e.ProcessedStatus == "Warning" || e.IsVerified == false)
            .Select(e => new CriticalAssetsDto
            {
                AssetId = e.Assets.Id,
                AssetSerial = e.Assets.AssetSerial,
                AssetType = e.Assets.AssetType,
                UnitName = e.Assets.Units.UnitName,
                Sector = e.Assets.Units.Sector,
                ProcessedStatus = e.ProcessedStatus,
                IsVerified = e.IsVerified,
                LastUpdate = e.LastUpdate,

            }).ToListAsync();

        return await result;
    }

    //GET `/api/reports/unit/{unitId}/assets`
    public async Task<IEnumerable<AssetsStatusDto>?> GetAllAssetsStatusByUnitAsync(int unitId)
    {

        //first check if unit exists

        var unitExists = await _context.Units.FirstOrDefaultAsync(e => e.Id == unitId);

        if (unitExists == null)
        {
            return null; //will be 404
        }

        //cehck if the unit have assetes
        
       //its check no teh assets not be null! good one!
        var allReqAssets = await _context.Assets.Where(e => e.UnitId == unitId && e.AssetLiveStatus!=null)
            .Select(e => new AssetsStatusDto
            {
                AssetId = e.Id,
                AssetSerial = e.AssetSerial,
                AssetType = e.AssetType,
                ProcessedStatus = e.AssetLiveStatus.ProcessedStatus,
                IsVerified = e.AssetLiveStatus.IsVerified,
                LastUpdate = e.AssetLiveStatus.LastUpdate
            }).ToListAsync();

        return allReqAssets;

    }
    //summarize all
    public async Task<IEnumerable<SummarizeUnitsDto>> SummaryAllUnitsAsync()
    {
        var result = _context.Units.
            Select(e => new SummarizeUnitsDto
            {
                UnitId = e.Id,
                UnitName = e.UnitName,
                Sector = e.Sector,
                TotalAssets = e.Assets.Count,
                StableAssets = e.Assets.Count(e => e.AssetLiveStatus.ProcessedStatus == "Stable"),
                WarningAssets = e.Assets.Count(e => e.AssetLiveStatus.ProcessedStatus == "Warning"),
                UnverifiedAssets = e.Assets.Count(e => e.AssetLiveStatus.IsVerified == false),
            }).ToListAsync();

        return await result;

    }




}