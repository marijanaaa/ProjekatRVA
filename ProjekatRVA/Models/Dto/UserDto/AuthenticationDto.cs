namespace ProjekatRVA.Models.Dto.UserDto
{
    public class AuthenticationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
