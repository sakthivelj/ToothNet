namespace ToothNet.Models
{
    public class PatientResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte[]? TeethImage { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Problem { get; set; } = string.Empty;
        public DateTime PhotoDate { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;

    }
}
