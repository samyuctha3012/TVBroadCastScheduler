using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.Domain.IServices
{
    public interface IShowService
    {
        List<ShowsModel> GetShowsForCurrentWindow();

        Task AddShowAsync(ShowsModel show);

        Task UpdateShowAsync(ShowsModel show);

        Task DeleteShowAsync(int id);

        bool IsTimeSlotAvailable(TimeSpan startTime, TimeSpan endTime);

        ShowsModel GetById(int id);

        Task<ShowsModel> GetShowByIdAsync(int id);


        Task ApproveShowAsync(int id);



    }
}
