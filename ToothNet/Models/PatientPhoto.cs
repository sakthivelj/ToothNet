using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToothNet.Models
{
    public class PatientPhoto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile? ImageFile { get; set; }
        public DateTime AddedIn { get; set; } 
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
