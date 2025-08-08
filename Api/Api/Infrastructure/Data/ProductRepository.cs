using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Dapper;
using Npgsql;

namespace Api.Infrastructure.Data
{

    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        public ProductRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Default");
        }

        private NpgsqlConnection GetConn() => new(_connectionString);

        public async Task<Guid> CreateAsync(Product product)
        {
            const string sql = @"INSERT INTO products (id, name, price, created_at)
                             VALUES (@Id, @Name, @Price, @CreatedAt)";
            product.Id = Guid.NewGuid();
            product.CreatedAt = DateTime.UtcNow;
            using var conn = GetConn();
            await conn.ExecuteAsync(sql, product);
            return product.Id;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            const string sql = "DELETE FROM products WHERE id = @Id";
            using var conn = GetConn();
            var affected = await conn.ExecuteAsync(sql, new { Id = id });
            return affected > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            const string sql = "SELECT id, name, price, created_at FROM products ORDER BY created_at DESC";
            using var conn = GetConn();
            return await conn.QueryAsync<Product>(sql);
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT id, name, price, created_at FROM products WHERE id = @Id";
            using var conn = GetConn();
            return await conn.QuerySingleOrDefaultAsync<Product?>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            const string sql = @"UPDATE products SET name = @Name, price = @Price WHERE id = @Id";
            using var conn = GetConn();
            var affected = await conn.ExecuteAsync(sql, new { product.Name, product.Price, product.Id });
            return affected > 0;
        }
    }

}
