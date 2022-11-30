namespace FuelProject.Domain.DTos
{
    public class LoginUserDto
    {
        public LoginUserDto(string id, string token)
        {
            Id = id;
            Token = token;
        }

        public string Id { get; set; } = string.Empty;
        public string Token { get; set; }=string.Empty;
    }
}
