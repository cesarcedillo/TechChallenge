namespace Backend.TechChallenge.Application.Models.User
{
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public decimal Money { get; set; }
    }
}
