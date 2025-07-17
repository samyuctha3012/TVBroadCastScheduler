using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.DAL.Context;
using TVBroadcast.Domain.IRepository;
using TVBroadcast.Domain.Models;

namespace TVBroadcast.DAL.Repositories
{
    public class ShowRepository : IShowRepository
    {
        private readonly AppDbContext _context;

        public ShowRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<ShowsModel> GetShowsByExactTimeSlots(List<TimeSpan> slotTimes)
        {
            return _context.Shows
                .Where(s => slotTimes.Contains(s.Time))
                .OrderBy(s => s.Time)
                .ToList();
        }

        public async Task AddShowAsync(ShowsModel show)
        {
            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateShowAsync(ShowsModel show)
        {
            var existingShow = await _context.Shows.FindAsync(show.Id);
            if (existingShow != null)
            {
                existingShow.Title = show.Title;
                existingShow.Genre = show.Genre;
                existingShow.Description = show.Description;
                existingShow.Time = show.Time;
                existingShow.ApprovalStatus = show.ApprovalStatus;

                await _context.SaveChangesAsync(); // 🧸 save your updated story
            }
        }

        public async Task DeleteShowAsync(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                _context.Shows.Remove(show);
                await _context.SaveChangesAsync();
            }
        }


    }
}
