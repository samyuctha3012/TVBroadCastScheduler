using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.Models;
using TVBroadcast.DAL.Context;
using Microsoft.EntityFrameworkCore;


namespace TVBroadcast.DAL.Repositories
{
    public class ApproveRepository : IApproveRepository
    {
        private readonly AppDbContext _context;

        public ApproveRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShowsModel>> GetPendingShowsAsync()
        {
            return await _context.Shows
                .Where(s => s.ApprovalStatus == "Pending")
                .ToListAsync();
        }

        public async Task<ShowsModel?> GetShowByIdAsync(int id)
        {
            return await _context.Shows.FindAsync(id);
        }

        public async Task<bool> UpdateShowAsync(ShowsModel show)
        {
            _context.Shows.Update(show);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
