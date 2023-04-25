namespace ToothNet.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;        
        public DateTime BornIn { get; set; }
        public Allergys AllergyType { get; set; } = Allergys.None;
        public DateTime OrderedOn { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
