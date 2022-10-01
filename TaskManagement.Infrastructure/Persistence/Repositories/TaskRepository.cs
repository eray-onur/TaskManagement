using Microsoft.Extensions.Configuration;

using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;

using Task = TaskManagement.Domain.Entities.Task;
using TaskStatus = TaskManagement.Domain.Enums.TaskStatus;
using TaskType = TaskManagement.Domain.Enums.TaskType;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TaskRepository(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }


        public async Task<Task?> GetById(Guid taskId)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            Task? task = null;

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand($"SELECT * FROM tasks WHERE id = '{taskId}' and is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var createdDate = reader.GetDateTime(2);
                    var requiredByDate = reader.GetDateTime(3);
                    var description = reader.GetString(4);
                    var status = reader.GetInt16(5);
                    var type = reader.GetInt16(6);
                    var assignedTo = reader.GetFieldValue<Guid?>(7);
                    var nextActionDate = reader.GetFieldValue<DateTime?>(8);

                    task = new Task(id, createdDate, description, (TaskStatus)status, (TaskType)type, assignedTo, nextActionDate);
                }
            }
            await conn.CloseAsync();

            return task;
        }

        public async Task<IEnumerable<Task>> GetAll()
        {
            List<Task> tasks = new List<Task>();
            string connString = _configuration.GetConnectionString("MasterDB");

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM tasks where is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var createdDate = reader.GetDateTime(2);
                    var requiredByDate = reader.GetDateTime(3);
                    var description = reader.GetString(4);
                    var status = reader.GetInt16(5);
                    var type = reader.GetInt16(6);
                    var assignedTo = reader.GetFieldValue<Guid?>(7);
                    var nextActionDate = reader.GetFieldValue<DateTime?>(8);

                    var task = new Task(id, createdDate, description, (TaskStatus) status, (TaskType) type, assignedTo, nextActionDate);
                    tasks.Add(task);
                }
            }
            await conn.CloseAsync();
            return tasks;
        }

        public async Task<Task> Insert(Task task)
        {
            User? userToAssign = null;
            if(task.AssignedTo.HasValue)
            {
                userToAssign = await _userRepository.GetById(task.AssignedTo.Value);
            }

            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("INSERT INTO tasks (id, created_date, required_by_date, description, task_status, type, assigned_to) VALUES ($1, $2, $3, $4 ,$5, $6, $7)", conn))
            {
                cmd.Parameters.AddWithValue(task.Id);
                cmd.Parameters.AddWithValue(task.CreatedDate);
                cmd.Parameters.AddWithValue(task.RequiredByDate);
                cmd.Parameters.AddWithValue(task.Description);
                cmd.Parameters.AddWithValue((int) task.Status);
                cmd.Parameters.AddWithValue((int) task.Type);
                cmd.Parameters.AddWithValue(userToAssign != null ? userToAssign.Id : DBNull.Value);
                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return task;
        }

        public async Task<Task> Update(Task task)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("UPDATE tasks SET created_date = $1, required_by_date = $2, description = $3, task_status = $4, type = $5, assigned_to = $6, next_action_date = $7 where id = $8", conn))
            {
                cmd.Parameters.AddWithValue(task.CreatedDate);
                cmd.Parameters.AddWithValue(task.RequiredByDate);
                cmd.Parameters.AddWithValue(task.Description);
                cmd.Parameters.AddWithValue((int)task.Status);
                cmd.Parameters.AddWithValue((int)task.Type);
                cmd.Parameters.AddWithValue(task.AssignedTo.HasValue ? task.AssignedTo.Value : DBNull.Value);
                cmd.Parameters.AddWithValue(task.NextActionDate.HasValue ? task.NextActionDate.Value : DBNull.Value);
                cmd.Parameters.AddWithValue(task.Id);
                var result = await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return task;
        }

        public async Task<Guid> SoftDelete(Guid taskId)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("UPDATE tasks SET is_deleted = TRUE where id = $1", conn))
            {
                cmd.Parameters.AddWithValue(taskId);
                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return taskId;
        }

       
    }
}
