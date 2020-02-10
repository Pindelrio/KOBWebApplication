using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KOBWebApplication.Data;
using KOBWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace KOBWebApplication.Services
{
    public class RepertorieService : IRepertorieService
    {
        private readonly ApplicationDbContext _context;

        public RepertorieService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Repertorie[]> GetRepertorieListAsync()
        {
            var list = await _context.Repertorie.OrderBy(x=>x.Index).ToArrayAsync();
            return (list);
        }

        public async Task<bool> AddItemAsync(Repertorie newItem)
        {
            if (newItem.SongId == null &&  string.IsNullOrEmpty(newItem.Comments))
                return true;

            newItem.Index = _context.Repertorie.Count() + 1;

            _context.Repertorie.Add(newItem);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        //---- Get Current Repertorie
        public async Task<Repertorie> GetCurrentRepertorieAsync()
        {
            if (_context.Repertorie.Count()>0)
            {
                var current = await _context.Repertorie.Where(x => x.IsCurrent == true).FirstOrDefaultAsync();
                return current;
            }
            else
                return new Repertorie() { Comments = "No current song", SongId =0 };
        }

        //---- Mark as current song
        public async Task<bool> MarkDoneAsync(Guid id)
        {
            //---- Mark all to false
            var items = await _context.Repertorie
                .Where(x => x.IsCurrent == true).ToArrayAsync();
            if (items.Length > 0)
            {
                foreach (var i in items)
                {
                    i.IsCurrent = false;
                }
            }

            var item = await _context.Repertorie
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();

            if (item == null) return false;
            item.IsCurrent = true;
            var saveResult = await _context.SaveChangesAsync();
            return true; //return always true
        }

        public async Task<bool> MoveUpAsync(int id)
        {
            if (id > 1)
            {
                var item2move = await _context.Repertorie
                .Where(x => x.Index == id)
                .SingleOrDefaultAsync();

                var itemUp = await _context.Repertorie
                .Where(x => x.Index == id - 1)
                .SingleOrDefaultAsync();

                if (itemUp != null)
                {
                    itemUp.Index = item2move.Index;
                    item2move.Index = itemUp.Index - 1;
                }
                var saveResult = await _context.SaveChangesAsync();
                return saveResult == 2;
            }
            return true;
        }
        public async Task<bool> MoveDownAsync(int id)
        {

            var item2move = await _context.Repertorie
            .Where(x => x.Index == id)
            .SingleOrDefaultAsync();

            var itemDown = await _context.Repertorie
            .Where(x => x.Index == id + 1)
            .SingleOrDefaultAsync();

            if (itemDown != null)
            {
                itemDown.Index = item2move.Index;
                item2move.Index = itemDown.Index + 1;
            }
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 2;

        }

        public async Task<bool> RemoveItemAsync(Guid id)
        {
            var item = await _context.Repertorie
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            _context.Remove(item);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        public async Task<Task> RemoveAllItemsAsync()
        {
             foreach(var i in _context.Repertorie)
            {
                _context.Remove(i);
            }
            var saveResult = await _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
        
    }
}
