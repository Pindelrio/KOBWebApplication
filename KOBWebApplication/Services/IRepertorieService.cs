using KOBWebApplication.Models;
using System;
using System.Threading.Tasks;

namespace KOBWebApplication.Services
{
    public interface IRepertorieService
    {
        Task<Repertorie[]> GetRepertorieListAsync();
        Task<bool> AddItemAsync(Repertorie newItem);
        Task<Repertorie> GetCurrentRepertorieAsync();
        Task<bool> MarkDoneAsync(Guid id);

        Task<bool> MoveUpAsync(int id);
        Task<bool> MoveDownAsync(int id);
        Task<bool> RemoveItemAsync(Guid id);

        Task <Task> RemoveAllItemsAsync();

    }
}
