using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TVBroadcast.DAL.Repositories;
using TVBroadcast.Domain.Models;
using TVBroadcast.Domain.IServices;
using TVBroadcast.Domain.IRepository;


namespace TVBroadcast.BLL.Services
{
    public class ShowService : IShowService
    {
        private readonly IShowRepository _showRepository;

        public ShowService(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        public List<ShowsModel> GetShowsForCurrentWindow()
        {

            TimeSpan now = DateTime.Now.TimeOfDay;
            TimeSpan currentSlot = new TimeSpan(now.Hours, now.Minutes < 30 ? 0 : 30, 0);


            List<TimeSpan> slotTimes = new List<TimeSpan>();
            for (int i = -4; i <= 4; i++)
            {
                TimeSpan slot = currentSlot.Add(TimeSpan.FromMinutes(i * 30));
                if (slot >= TimeSpan.Zero && slot < TimeSpan.FromHours(24))
                {
                    slotTimes.Add(slot);
                }
            }

            return _showRepository.GetShowsByExactTimeSlots(slotTimes); ;
        }
        public async Task AddShowAsync(ShowsModel show)
        {
            await _showRepository.AddShowAsync(show);
        }

        //for editing the form
        public async Task UpdateShowAsync(ShowsModel show)
        {
            await _showRepository.UpdateShowAsync(show); // 💕 call to repository
        }

        //for deleting the shows from database

        public ShowsModel GetById(int id)
        {
            return _showRepository.GetById(id); // Simple and sweet 🍬
        }


        public async Task DeleteShowAsync(int id)
        {
            await _showRepository.DeleteShowAsync(id);
        }


        public List<ShowTimeDTO> GetShowTimes()
        {
            return _showRepository.GetShowTimes();
        }

        public bool IsTimeSlotAvailable(TimeSpan startTime, TimeSpan endTime)
        {
            return _showRepository.IsTimeSlotAvailable(startTime, endTime);
        }

        public async Task<ShowsModel> GetShowByIdAsync(int id)
        {
            return await _showRepository.GetByIdAsync(id); // Not FindAsync!
        }


       
        public async Task ApproveShowAsync(int id)
        {
            await _showRepository.ApproveShowAsync(id);
        }



    }
}
