using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize] // to make a Registration
        public ActionResult Create()

        {
            var viewmodel = new GigFormViewModel
            {
                 Genres = _context.Genres.ToList()
            };
            return View(viewmodel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var UserId = User.Identity.GetUserId();
            var gigs = _context.Attendances
                .Where(a => a.AttendeeId == UserId)
                .Select(g => g.Gig)
                .Include(g => g.Artist)
                 .Include(g => g.Genre)
                .ToList();

            var GigViewModel = new HomeViewModel()
            {
                Upcominggig = gigs,
                ShowAction = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending"
            };
            return View("Gigs",GigViewModel);



        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                 
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
            }


            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime =  viewModel.GetDateTime(),
                GenreId =  viewModel.Genre,
                Venue = viewModel.Venue

            };
            _context.Gigs.Add(gig);
            _context.SaveChanges();
            return RedirectToAction("Index" ,"Home");
        }
    }
}