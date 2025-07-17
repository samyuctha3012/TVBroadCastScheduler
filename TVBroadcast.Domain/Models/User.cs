using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TVBroadcast.Domain.Models
{
    
        public class User
        {
            public int Id { get; set; } // 🧠 Auto-increment primary key

            public string FullName { get; set; }

            public string Email { get; set; }

            public string PasswordHash { get; set; }

            public int RoleId { get; set; } // 🍼 FK to Roles table

            public Role Role { get; set; } // 🎀 Navigation property (optional but cute)
        }
    

}
