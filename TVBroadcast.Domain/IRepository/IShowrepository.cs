using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.Domain.IRepository
{
    public interface IShowRepository
    {
        List<ShowsModel> GetShowsByExactTimeSlots(List<TimeSpan> slotTimes);

        Task AddShowAsync(ShowsModel show);

        Task UpdateShowAsync(ShowsModel show);

        Task DeleteShowAsync(int id);

        List<ShowTimeDTO> GetShowTimes();

        bool IsTimeSlotAvailable(TimeSpan startTime, TimeSpan endTime);

        ShowsModel GetById(int id);

        Task<List<ShowsModel>> GetPendingShowsAsync();
        Task ApproveShowAsync(int id);

        Task<ShowsModel> GetByIdAsync(int id);


        Task<ShowsModel> GetShowByIdAsync(int id);
    }
}
