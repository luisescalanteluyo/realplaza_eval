using Api.Application.Services;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegisterAsync(string username, string password);
        Task<AuthResult?> LoginAsync(string username, string password);
    }
}
