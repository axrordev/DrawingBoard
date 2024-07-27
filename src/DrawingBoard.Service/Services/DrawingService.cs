using DrawingBoard.Data.DataContexts;
using DrawingBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DrawingBoard.Service.Services;

public class DrawingService(AppDbContext context) : IDrawingService
{
    public async ValueTask<List<Drawing>> GetAllDrawingsAsync()
    {
        return await context.Drawings.ToListAsync();
    }

    public async ValueTask AddDrawingAsync(Drawing drawing)
    {
        context.Drawings.Add(drawing);
        await context.SaveChangesAsync();
    }
}

