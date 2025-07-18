using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.Domain.IServices
{
    public interface IApproveService
    {
        Task<List<ShowsModel>> GetPendingShowsAsync();
        Task<bool> ApproveRejectAsync(int id, string action, string comment);
    }
}
