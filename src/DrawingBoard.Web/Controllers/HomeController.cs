using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DrawingBoard.Web.Models;
using Microsoft.EntityFrameworkCore;
using DrawingBoard.Data.DataContexts;
using DrawingBoard.Domain.Entities;
using DrawingBoard.Web.Models.Drawings;
using DrawingBoard.Service.Services;

namespace DrawingBoard.Web.Controllers;

public class HomeController(IDrawingService drawingService, AppDbContext context) : Controller
{
    public async ValueTask<IActionResult> Index()
    {
        var drawings = await drawingService.GetAllDrawingsAsync(); // This should return List<Drawing>
        var drawingViewModels = drawings.Select(d => new DrawingViewModel
        {
            Id = d.Id,
            Name = d.Name,
            Data = d.Data,
            UserName = d.UserName
        }).ToList();

        return View(drawingViewModels);

    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateDrawing(string drawingName, string userName)
    {
        var drawing = new Drawing
        {
            Name = drawingName,
            UserName = userName,
            Data = "", // Initially empty or some default value
            CreatedAt = DateTime.UtcNow
        };

        await drawingService.AddDrawingAsync(drawing);

        return RedirectToAction("Index");
    }

    public IActionResult Draw(long id)
    {
        var drawing = context.Drawings.Find(id);
        if (drawing == null) return NotFound();
        return View(drawing);
    }
    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}