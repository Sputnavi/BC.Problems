using System.ComponentModel.DataAnnotations;

namespace BC.Problems.Boundary.Request
{
    public class BicycleForCreateOrUpdateModel
    {
        [Required]
        [MaxLength(255)]
        public string Model { get; set; }
        [MaxLength(255)]
        public string SerialNumber { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
