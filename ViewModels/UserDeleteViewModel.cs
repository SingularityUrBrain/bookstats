using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStats.ViewModels
{
    public class UserDeleteViewModel
    {
        public string Id { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
    }
}
