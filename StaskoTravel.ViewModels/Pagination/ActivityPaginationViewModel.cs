using StaskoTravel.ViewModels.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaskoTravel.ViewModels.Pagination
{
    public class ActivityPaginationViewModel
    {
        public List<ActivityIndexViewModel> Activities { get; set; } = new();

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
