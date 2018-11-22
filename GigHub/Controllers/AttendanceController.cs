using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendanceController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendanceController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendaceDto dto)
        {
            var userId = User.Identity.GetUserId();

            // that mean if i click Going by the same username and the gig and want to click again will be error
            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
                return BadRequest("The Attendance Already exists");

            var attendances = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId =userId
            };
            _context.Attendances.Add(attendances);
            _context.SaveChanges();
            return Ok();
        }
    }
}
