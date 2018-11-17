using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Following
    {
        [Key]
        [Column (Order = 1)]
        public string followerId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string followeeId { get; set; }

        public ApplicationUser follower { get; set; }
        public ApplicationUser followee { get; set; }
    }
}