using Microsoft.Extensions.Configuration;

using Npgsql;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Abstract;
using TaskComment = TaskManagement.Domain.Entities.TaskComment;

namespace TaskManagement.Infrastructure.Persistence.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ITaskRepository _taskRepository;

        public TaskCommentRepository(IConfiguration configuration, ITaskRepository taskRepository)
        {
            _configuration = configuration;
            _taskRepository = taskRepository;
        }

        public async Task<TaskComment> GetById(Guid commentId)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            TaskComment? taskComment = null;

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand($"SELECT * FROM task_comments WHERE id = '{commentId}' and is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var id = reader.GetGuid(0);
                    var taskId = reader.GetGuid(1);
                    var dateAdded = reader.GetDateTime(2);
                    var commentType = reader.GetInt16(3);
                    var comment = reader.GetString(4);
                    var reminderDate = reader.GetDateTime(5);

                    taskComment = new TaskComment(id, taskId, dateAdded, comment, (CommentType) commentType, reminderDate);
                }
            }
            await conn.CloseAsync();

            if (taskComment == null)
                throw new Exception($"Failed to find the task comment by id {commentId}");

            return taskComment;
        }

        public async Task<IEnumerable<TaskComment>> GetAll()
        {
            List<TaskComment> taskComments = new List<TaskComment>();
            string connString = _configuration.GetConnectionString("MasterDB");

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM task_comments where is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var taskId = reader.GetGuid(1);
                    var dateAdded = reader.GetDateTime(2);
                    var commentType = reader.GetInt16(3);
                    var comment = reader.GetString(4);
                    var reminderDate = reader.GetDateTime(5);

                    var taskComment = new TaskComment(id, taskId, dateAdded, comment, (CommentType)commentType, reminderDate);
                    taskComments.Add(taskComment);
                }
            }
            await conn.CloseAsync();
            return taskComments;
        }

        public async Task<IEnumerable<TaskComment>> GetAllCommentsByTask(Guid belongingTaskId)
        {
            List<TaskComment> taskComments = new List<TaskComment>();
            string connString = _configuration.GetConnectionString("MasterDB");

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand($"SELECT * FROM task_comments where task_id = '{belongingTaskId}' and is_deleted = FALSE", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {

                    var id = reader.GetGuid(0);
                    var taskId = reader.GetGuid(1);
                    var dateAdded = reader.GetDateTime(2);
                    var commentType = reader.GetInt16(3);
                    var comment = reader.GetString(4);
                    var reminderDate = reader.GetDateTime(5);

                    var taskComment = new TaskComment(id, taskId, dateAdded, comment, (CommentType)commentType, reminderDate);
                    taskComments.Add(taskComment);
                }
            }
            await conn.CloseAsync();
            return taskComments;
        }

        public async Task<TaskComment> Insert(TaskComment entity)
        {
            string connString = _configuration.GetConnectionString("MasterDB");

            // Validate if a task ID with given entity's information exists.
            Domain.Entities.Task? foundTask = await _taskRepository.GetById(entity.TaskId);

            if(foundTask == null)
            {
                throw new Exception("Cannot find a valid task with given ID for the comment to be assigned.");
            }
            

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert the new comment to the database.
            await using (var cmd = new NpgsqlCommand("INSERT INTO task_comments (id, task_id, date_added, comment_type, comment, reminder_date) VALUES ($1, $2, $3, $4 ,$5, $6)", conn))
            {
                cmd.Parameters.AddWithValue(entity.Id);
                cmd.Parameters.AddWithValue(foundTask.Id);
                cmd.Parameters.AddWithValue(entity.DateAdded);
                cmd.Parameters.AddWithValue((int) entity.CommentType);
                cmd.Parameters.AddWithValue(entity.Comment);
                cmd.Parameters.AddWithValue(entity.ReminderDate);

                await cmd.ExecuteNonQueryAsync();
            }

            // Update the assigned task's next action date with the new comment's reminder date.
            await using (var cmd = new NpgsqlCommand("UPDATE tasks SET next_action_date = $1 where id = $2", conn))
            {
                cmd.Parameters.AddWithValue(entity.ReminderDate);
                cmd.Parameters.AddWithValue(entity.TaskId);
                await cmd.ExecuteNonQueryAsync();
            }

            await conn.CloseAsync();

            return entity;
        }

        public async Task<TaskComment> Update(TaskComment entity)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("UPDATE task_comments SET task_id = $1, date_added = $2, comment_type = $3, comment = $4, reminder_date = $5 where id = $6", conn))
            {
                cmd.Parameters.AddWithValue(entity.TaskId);
                cmd.Parameters.AddWithValue(entity.DateAdded);
                cmd.Parameters.AddWithValue((int)entity.CommentType);
                cmd.Parameters.AddWithValue(entity.Comment);
                cmd.Parameters.AddWithValue(entity.ReminderDate);
                cmd.Parameters.AddWithValue(entity.Id);

                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return entity;
        }

        public async Task<Guid> SoftDelete(Guid commentId)
        {
            string connString = _configuration.GetConnectionString("MasterDB");
            await using var conn = new NpgsqlConnection(connString);

            await conn.OpenAsync();
            await using (var cmd = new NpgsqlCommand("UPDATE task_comments SET is_deleted = TRUE where id = $1", conn))
            {
                cmd.Parameters.AddWithValue(commentId);
                await cmd.ExecuteNonQueryAsync();
            }
            await conn.CloseAsync();

            return commentId;
        }
    }
}
