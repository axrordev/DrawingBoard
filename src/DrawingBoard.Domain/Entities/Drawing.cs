using Task.Domain.Commons;

namespace DrawingBoard.Domain.Entities;

public class Drawing : Auditable
{
    public string Name { get; set; } // Name of the drawing
    public string Data { get; set; } // Data URL or JSON
    public string UserName { get; set; }
}