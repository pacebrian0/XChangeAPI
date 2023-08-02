namespace XChangeAPI.Models.DTO
{
    public class UserRegisterDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }

    public class UserResponseDTO
    {
        public required int ID { get; set; }
        public required string Token { get; set; }
    }

    public class UserLoginDTO
    {
        public required string Email { get; set; }
        public required string Password { get; set; }

    }

    public class UserPost
    {
        public string name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;


    }

    public class UserPut
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string modifiedBy { get; set; } = string.Empty;
        public char status { get; set; } = 'A';


    }
}
