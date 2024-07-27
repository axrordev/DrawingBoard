using DrawingBoard.Data.DataContexts;
using DrawingBoard.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DrawingBoard.Web.Hubs;

public class DrawingHub() : Hub
{
    public async ValueTask SendDrawing(string drawingId, string drawingData)
    {
        await Clients.Others.SendAsync("ReceiveDrawing", drawingId, drawingData);
    }
}
