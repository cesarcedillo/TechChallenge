namespace Backend.TechChallenge.Domain
{
    public enum UserTypes
    {
        Normal,
        SuperUser,
        Premium
    }

    public class User
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public UserTypes UserType { get; set; }
        public decimal Money { get; set; }
    }
}
