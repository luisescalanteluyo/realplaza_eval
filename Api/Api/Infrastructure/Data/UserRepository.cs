using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Dapper;
using Npgsql;


namespace Api.Infrastructure.Data
{

public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration config) => _connectionString = config.GetConnectionString("Default");
        private NpgsqlConnection GetConn() => new(_connectionString);

        public async Task<Guid> CreateAsync(User user)
        {
            const string sql = @"INSERT INTO users (id, username, password, role, created_at)
                             VALUES (@Id, @Username, @Password, @Role, @CreatedAt)";
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            using var conn = GetConn();
            await conn.ExecuteAsync(sql, user);
            return user.Id;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            const string sql = "SELECT id, username, password, role, created_at FROM users WHERE username = @Username";
            using var conn = GetConn();
            return await conn.QuerySingleOrDefaultAsync<User?>(sql, new { Username = username });
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT id, username, password, role, created_at FROM users WHERE id = @Id";
            using var conn = GetConn();
            return await conn.QuerySingleOrDefaultAsync<User?>(sql, new { Id = id });
        }
    }

}
