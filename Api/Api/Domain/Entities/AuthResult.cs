namespace Api.Domain.Entities
{
    public class AuthResult
    {
        public string? Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
