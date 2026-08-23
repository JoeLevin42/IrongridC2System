
using DashbordApi.Data;
using DashbordApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DashbordApi.Repositories;

public class DashbordRepository
{
    private readonly ApplicationDbContext _context;
    public DashbordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AssetLiveStatus>> GetAllAssetLiveAsync()
    {
        return await _context.AssetLiveStatus.ToListAsync();
    }
}