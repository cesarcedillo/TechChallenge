namespace Backend.TechChallenge.Application.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; } = string.Empty;
    }
}
