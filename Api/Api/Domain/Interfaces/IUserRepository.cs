using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<Guid> CreateAsync(User user);
        Task<User?> GetByIdAsync(Guid id);
    }
}
