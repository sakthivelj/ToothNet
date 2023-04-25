using System.ComponentModel.DataAnnotations.Schema;

namespace ToothNet.Models
{
    public class PatientPhoto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile XRayImage { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime AddedIn { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
