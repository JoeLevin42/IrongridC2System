using DashbordApi.Models;
using DashbordApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DashbordApi.Controllers;

[ApiController]
[Route("assets")]
public class AssetsController : ControllerBase
{
    private readonly AssetsRepository _repo;
    public AssetsController(AssetsRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("{id}")] //get by id
    public async Task<ActionResult<AssetsReadingDto>> GetAssetsByIdAsync(int id)
    {
        var result = await _repo.GetAssetesByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    //new unit
    [HttpPost("units")]
    public async Task<ActionResult<Units?>> AddNewUnitAsync(CreateUnitDto newUnit) //mybe change to a dto?
    {
        var createResult = await _repo.AddNewUnitAsync(newUnit);

        if (createResult == null)
        {
            return BadRequest();
        }

        return StatusCode(201);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<AssetesUpdateDto?>> UpdateAssetAsync(int id, AssetesUpdateDto updatedAsset)
    {
        var updatedResultObj = await _repo.UpdateAssetAsync(id, updatedAsset);
        if (updatedResultObj == null)
        {
            return BadRequest(); //id the unit assign not exists
        }
        //if (updatedResult == null)
        //{
        //    return NotFound(); // id the assets itseld not found
        //}

        return Ok(updatedResultObj);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAssetsAsync(int id)
    {
        var isDeleted = await _repo.DeleteAssetsAsync(id);

        if (isDeleted == false)
        {
            return NotFound();
        }
        return NoContent();
    }
}