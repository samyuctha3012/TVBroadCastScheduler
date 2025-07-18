using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.Domain.IRepository
{

    public interface IApproveRepository
    {
        Task<List<ShowsModel>> GetPendingShowsAsync();
        Task<ShowsModel?> GetShowByIdAsync(int id);
        Task<bool> UpdateShowAsync(ShowsModel show);
    }

}
