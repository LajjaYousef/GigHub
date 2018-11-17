using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();



            if (_context.Followings.Any(f => f.followeeId == userId && f.followeeId == dto.followeeId))

                return BadRequest("Following Already exists");


            var following = new Following
            {
                followerId = userId,
                followeeId = dto.followeeId

            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }


    }
}
