using BookStats.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.ViewModels
{
    public class UserDeatailsViewModel
    {
        public User User { get; set; }

        public bool IsActive { get; set; } = false;
    }
}
