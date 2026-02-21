using Application.Abstracts.Repositories;
using Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;


namespace Persistance.Repositories;

public class DistrictRepository : GenericRepository<District, int>, IDistrictRepository
{
    private readonly BinaLiteDbContext _context;
    public DistrictRepository(BinaLiteDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByNameDistrictAsync(string name, int excludeId, CancellationToken ct)
    {
        name = name.Trim();

        return await _context.Cities
            .AnyAsync(c =>
                c.Id != excludeId &&
                c.Name.ToLower() == name.ToLower(),
                ct);
    }
}
