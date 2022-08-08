namespace ChatServer.Dtos.User
{
    public class ClientUserDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
    }

    public class UpdateUserDto
    {
        public string Email { get; set; }

        public string Username { get; set; }
    }
}
