using KOBWebApplication.Models;
using KOBWebApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KOBWebApplication.Controllers
{
    public class RepertorieListController : Controller
    {
        private readonly IRepertorieService _repertoryService;

        public RepertorieListController(IRepertorieService repertorieService)
        {
            _repertoryService = repertorieService;
        }

        //---- List Repertorie
        public async Task<IActionResult> Index()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var list = await _repertoryService.GetRepertorieListAsync();

            var model = new RepertorieViewModel()
            {
                RepertorieList = list
            };

            return View(model);
        }

        //---- Add Items to list
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(Repertorie newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _repertoryService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty) 
            {
                return RedirectToAction("Index");
            }
            var successful = await _repertoryService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveUp(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var successful = await _repertoryService.MoveUpAsync(id);
            if (!successful)
            {
                return BadRequest("Could not move item up.");
            }
            return RedirectToAction("Index");
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MoveDown(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var successful = await _repertoryService.MoveDownAsync(id);
            if (!successful)
            {
                return BadRequest("Could not move item down.");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveItem(Guid id)
        {
            if (id == Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var successful = await _repertoryService.RemoveItemAsync(id);
            if (!successful)
            {
                return BadRequest("Could not remove item down.");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveAllItems()
        {
            var successful = _repertoryService.RemoveAllItemsAsync();
            
            return RedirectToAction("Index");
        }
    }
}