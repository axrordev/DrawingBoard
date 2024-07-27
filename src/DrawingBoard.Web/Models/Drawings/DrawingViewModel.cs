namespace DrawingBoard.Web.Models.Drawings;

public class DrawingViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } // Name of the drawing
    public string Data { get; set; } // Data URL or JSON
    public string UserName { get; set; }
}
