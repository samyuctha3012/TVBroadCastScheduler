using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.IServices;
using TVBroadcast.Domain.Models;


namespace TVBroadcast.BLL.Services
{
    public class ApproveService : IApproveService
    {
        private readonly IApproveRepository _approveRepository;

        public ApproveService(IApproveRepository approveRepository)
        {
            _approveRepository = approveRepository;
        }

        public async Task<List<ShowsModel>> GetPendingShowsAsync()
        {
            return await _approveRepository.GetPendingShowsAsync();
        }

        public async Task<bool> ApproveRejectAsync(int id, string action, string comment)
        {
            var show = await _approveRepository.GetShowByIdAsync(id);
            if (show == null)
                return false;

            show.ApprovalStatus = action == "Approve" ? "Approved" :
                                  action == "Reject" ? "Rejected" :
                                  show.ApprovalStatus;

            // Optionally save the comment if you have a column
            // show.Comment = comment;

            return await _approveRepository.UpdateShowAsync(show);
        }
    }
}
