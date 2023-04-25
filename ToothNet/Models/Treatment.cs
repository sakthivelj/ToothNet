namespace ToothNet.Models
{
    public class Treatment
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Cure { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
