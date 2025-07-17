using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVBroadcast.Domain.Models
{
    public class ShowsModel
    {
        public int Id { get; set; }               // Unique ID
        public TimeSpan Time { get; set; }        // Start time of the show
        public string Title { get; set; }         // Name of the show
        public string Genre { get; set; }         // Genre like Drama, Comedy, etc.
        public string Description { get; set; }   // Short description about the show
        public int DurationMinutes { get; set; }  // How long the show runs (in minutes)
        public string ApprovalStatus { get; set; } // Approved / Pending / Rejected  // Optional: channel or source of the show
    }
}
