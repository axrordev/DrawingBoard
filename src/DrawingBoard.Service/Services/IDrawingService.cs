using DrawingBoard.Domain.Entities;

namespace DrawingBoard.Service.Services;

public interface IDrawingService
{
    ValueTask<List<Drawing>> GetAllDrawingsAsync();
    ValueTask AddDrawingAsync(Drawing drawing);
}

