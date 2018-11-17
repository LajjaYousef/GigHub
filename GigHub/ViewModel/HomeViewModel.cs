using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> Upcominggig { get; set; }
        public bool ShowAction { get; set; }
        public string Heading { get; set; }
    }
}