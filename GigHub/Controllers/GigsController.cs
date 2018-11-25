using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System;
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
        } // Constructor ApplicationDbContext

        [Authorize] // to make a Registration
        // This action allow me to make a contain of page (venue,Data,time,Genre) to create the venue i will attend

        public ActionResult Create()

        {
            var viewmodel = new GigFormViewModel
            {
                 Genres = _context.Genres.ToList(),
                Heading = "Add Aa Gig"
            };
            return View("Gigform",viewmodel);
            /*
             the view Gigform send me to the textbox of action (venue,genre,data,time)..
             the viewmodel is a variable 
             */
        }



        // this action allow me to update data
        [Authorize] 
        public ActionResult Edit(int id)

        {
            var UserId = User.Identity.GetUserId();

            var gigs = _context.Gigs.Single(g => g.Id == id && g.ArtistId == UserId);

            var viewmodel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                id = gigs.Id,
                Genres = _context.Genres.ToList(),
                Date = gigs.DateTime.ToString("d MMM yyy"),
                Time = gigs.DateTime.ToString("HH:mm"),
                Genre = (byte)gigs.GenreId,
                Venue = gigs.Venue,
                

            };
            return View("Gigform",viewmodel);
        }

       
        // this action send me to the venue that is i will Upcoming it
        [Authorize]
        public ActionResult Mine()
        {
            var UserId = User.Identity.GetUserId();
            var gigs = _context.Gigs.
                Where(g => g.ArtistId == UserId && g.DateTime > DateTime.Now && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
            return View(gigs);
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
        public ActionResult Following()
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
                Heading = "Gigs I'm Following"
            };
            return View("Gigs", GigViewModel);



        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                 
                viewModel.Genres = _context.Genres.ToList();
                return View("Gigform", viewModel);
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
            return RedirectToAction("Mine" ,"Gigs");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                 
                viewModel.Genres = _context.Genres.ToList();
                return View("Gigform", viewModel);
            }

            var UserId = User.Identity.GetUserId();

            var gig = _context.Gigs.Single(g => g.Id == viewModel.id && g.ArtistId == UserId);

            gig.Venue = viewModel.Venue;
            gig.DateTime = viewModel.GetDateTime();
            gig.GenreId = viewModel.Genre;


            
            _context.SaveChanges();

            return RedirectToAction("Mine" ,"Gigs");
        }
    }
}