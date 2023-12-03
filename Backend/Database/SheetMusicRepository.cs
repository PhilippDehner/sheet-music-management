using Backend.DbContexts;
using Backend.Types;
using Microsoft.EntityFrameworkCore;

namespace Backend.Database;

public class SheetMusicRepository(SheetMusicManagementContext context)
{
	private readonly SheetMusicManagementContext _context = context;

	public async Task<IEnumerable<SheetMusic>> GetAll()
	{
		return await _context.SheetMusic.ToListAsync();
	}
}
