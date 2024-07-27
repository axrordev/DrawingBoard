using DrawingBoard.Data.DataContexts;
using DrawingBoard.Web.Models.Drawings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DrawingBoard.Web.Controllers
{
    public class DrawingController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        // GET: 
        public IActionResult Edit(long id)
        {
            var drawing = context.Drawings.Find(id);
            if (drawing == null)
            {
                return NotFound();
            }
            return View(new DrawingViewModel
            {
                Id = id,
                Name = drawing.Name,
                Data = drawing.Data,
                UserName = drawing.UserName
            });
        }

        [HttpPost]
        public IActionResult Edit(long id, DrawingViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var drawing = context.Drawings.Find(id);
                if (drawing == null)
                {
                    return NotFound();
                }

                drawing.Name = model.Name;
                drawing.UserName = model.UserName;
                context.Update(drawing);
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var drawing = context.Drawings.Find(id);
            if (drawing == null)
            {
                return NotFound();
            }

            context.Drawings.Remove(drawing);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
