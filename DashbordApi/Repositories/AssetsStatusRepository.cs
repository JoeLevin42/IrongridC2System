
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace DashbordApi.Repositories;

public class AssetsStatusRepository
{
    private readonly ApplicationDbContext _context;
    public AssetsStatusRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AssetsWithLastLiveDto>> GetAllAssetLiveAsync()
    {
        var dtoResultList = await _context.AssetLiveStatus
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

        return  dtoResultList;

    }

    public async Task<AssetsWithLastLiveDto?> GetAssetsLiveByIdAsync(int id)
    {
        var theAssetes = await _context.AssetLiveStatus.FirstOrDefaultAsync(e => e.AssetId == id);

        if (theAssetes == null)
        {
            return null;
        }
        var result =  _context.AssetLiveStatus.Where(e => e.AssetId == id)
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

         }).First();




        return result;


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