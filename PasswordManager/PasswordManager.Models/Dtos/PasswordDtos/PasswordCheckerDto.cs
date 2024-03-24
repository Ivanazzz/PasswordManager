namespace PasswordManager.Models.Dtos.PasswordDtos
{
    public class PasswordCheckerDto
    {
        private const int MaxPasswordScore = 5;

        public int MaxScore => MaxPasswordScore;

        public int YourScore { get; set; }

        public string Advice { get; set; }
    }
}
