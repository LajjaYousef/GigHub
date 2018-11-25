using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{

    // this class will be contain the Name of Artist , time of the Gig , type of the gig

    public class Gig
    {
        /*
         I use Type  ApplicationUser because i want to Know how ia performing the gig , when , where , and what genre it's
         
         the ApplicationUser is contain the user  so i use it
         
             
         */
        public int  Id { get; set; }

        public bool IsCanceled { get; set; }

        [Required]
        public string ArtistId { get; set; }


        public ApplicationUser Artist { get; set; }

        
        
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(225)]
        public string Venue { get; set; }


        // i use the Genre As The Forign Key to Attach the Gig With Genre Because Genre will Conatin the Name of Gig
        [Required]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }



            
    }

    
}