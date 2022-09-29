using Microsoft.Extensions.Configuration;

using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<User?> FindByEmail(string requiredEmail)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            User? user = null;

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand($"SELECT * FROM users WHERE email = '{requiredEmail}' and is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var email = reader.GetString(2);
                    var password = reader.GetString(3);

                    user = new User(id, email, password);
                }
            }
            await conn.CloseAsync();

            return user;
        }

        public async Task<User?> GetById(Guid userId)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            User? user = null;

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand($"SELECT * FROM users WHERE id = '{userId}' and is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var email = reader.GetString(2);
                    var password = reader.GetString(3);

                    user = new User(id, email, password);
                }
            }
            await conn.CloseAsync();

            return user;
        }

        public Task<IEnumerable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Insert(User entity)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO users (id, email, password) VALUES ($1, $2, $3)", conn))
            {
                cmd.Parameters.AddWithValue(entity.Id);
                cmd.Parameters.AddWithValue(entity.Email);
                cmd.Parameters.AddWithValue(entity.Password);
                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return entity;
        }

        public Task<User> Update(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> SoftDelete(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
