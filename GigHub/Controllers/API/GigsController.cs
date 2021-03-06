﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();

        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var Userid = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == Userid);

            if (gig.IsCanceled)
                return NotFound();

            gig.IsCanceled = true;
            _context.SaveChanges();
            return Ok();
        }
    }
}
