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

    }
}
