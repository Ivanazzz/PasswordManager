namespace PasswordManager.Models.Dtos.InfoDtos
{
    public class InfoGetDto
    {
        public int Id { get; set; }

        public string Website { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
