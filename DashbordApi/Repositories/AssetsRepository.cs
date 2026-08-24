
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DashbordApi.Repositories;

public class AssetsRepository
{
    private readonly ApplicationDbContext _context;
    public AssetsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    //GET `/api/assets/{id}` //later changes to an dto
    public async Task<Assets?> GetAssetesByIdAsync(int id)
    {
        var theAssets = await _context.Assets.FirstOrDefaultAsync(e => e.Id == id);

        if (theAssets == null)
        {
            return null;
        }

        return theAssets;
    }

    //POST `/api/assets/units` //later dto?
    public async Task<CreateUnitDto?> AddNewUnitAsync(CreateUnitDto newUnit)
    {
       
        //means thats valid 
        // CAN PUT HERE VALIDATIONS OR BUSSNIES LOGIC
        var newCreateUnit = new Units
        {
            UnitName = newUnit.UnitName,
            Sector = newUnit.Sector,
        };
        _context.Units.Add(newCreateUnit);
        await _context.SaveChangesAsync();

        return newUnit;
    }

    //PUT `/api/assets/{id}` // update ASSETES bool if success //dto for update
    public async Task<AssetesUpdateDto?> UpdateAssetAsync(int id, AssetesUpdateDto updatedAsset)
    {
        //first check if UnitId exists 

        var unitCheck = await _context.Units.FirstOrDefaultAsync(e => e.Id == updatedAsset.UnitId);

        if (unitCheck == null)
        {
            return null; //cant update beacuse unit not exists! //null will be BadRequest() - 400
        }

        //else search the assetes itsseld

        var existsAssetes = await _context.Assets.FirstOrDefaultAsync(e => e.Id == id);
        if (existsAssetes == null)
        {
            return null; //try to update not exists Assetes! // false will be NotFound() 404
        }
        //else

        existsAssetes.UnitId = updatedAsset.UnitId;
        existsAssetes.AssetSerial = updatedAsset.AssetSerial;
        existsAssetes.AssetType = updatedAsset.AssetType;
        existsAssetes.AssetType = updatedAsset.AssetType;

        await _context.SaveChangesAsync();
        return updatedAsset; // if we reache here so update success
    }

    //DELETE `/api/assets/{id}` //

    public async Task<bool> DeleteAssetsAsync(int id)
    {
        //search for it
        var theAssets = await _context.Assets.FirstOrDefaultAsync(e => e.Id == id);
        if (theAssets == null)
        {
            return false; //not found
        }

        _context.Assets.Remove(theAssets);
        await _context.SaveChangesAsync();
        return true; //successfully deleted!



    }







}
