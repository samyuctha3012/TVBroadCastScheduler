using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
                .Where(s => slotTimes.Contains(s.StartTime))
                .OrderBy(s => s.StartTime)
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
                existingShow.StartTime = show.StartTime;
                existingShow.EndTime = show.EndTime;
                existingShow.ApprovalStatus = show.ApprovalStatus;

                await _context.SaveChangesAsync(); // 🧸 save your updated story
            }
        }
        public ShowsModel GetById(int id)
        {
            return _context.Shows.FirstOrDefault(s => s.Id == id);
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

        public List<ShowTimeDTO> GetShowTimes()
        {
            return _context.Shows
                .Select(s => new ShowTimeDTO
                {
                    StartTime = s.StartTime,
                    EndTime = s.EndTime
                }).ToList();
        }

        public bool IsTimeSlotAvailable(TimeSpan startTime, TimeSpan endTime)
        {
            return !_context.Shows.Any(s =>
                startTime < s.EndTime && endTime > s.StartTime
            );
        }


        public async Task<List<ShowsModel>> GetPendingShowsAsync()
        {
            return await _context.Shows
                                 .Where(s => s.ApprovalStatus == "Pending")
                                 .ToListAsync();
        }

        public async Task ApproveShowAsync(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show != null)
            {
                show.ApprovalStatus = "Approved";
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ShowsModel> GetShowByIdAsync(int id)
        {
            return await _context.Shows.FindAsync(id);
        }

        public async Task<ShowsModel> GetByIdAsync(int id)
        {
            return await _context.Shows.FindAsync(id);
        }



    }
}
