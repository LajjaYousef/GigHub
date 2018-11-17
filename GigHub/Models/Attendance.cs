using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }


        // the key and order mean the GigId , AttendeeId is PrimeryKey (using Compsite key)
        // the GigId , AttendeeId Is ForigenKey also
        [Key]
        [Column (Order = 1)]
        public int GigId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}