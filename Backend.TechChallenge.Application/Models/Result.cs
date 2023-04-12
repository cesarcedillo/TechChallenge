namespace Backend.TechChallenge.Application.Model
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; } = string.Empty;
    }
}
