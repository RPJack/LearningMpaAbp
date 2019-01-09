using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using LearningMpaAbp.Tasks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tasks
{
    /// <summary>
    /// 实现IApplicationService，会自动帮助实现依赖注入
    /// </summary>
    public interface ITaskAppServices:IApplicationService
    {
        GetTasksOutput GetTasks(GetTaskInput input);

        void UpdateTask(UpdateTaskInput input);

        int CreateTask(CreateTaskInput input);

        Task<TaskDto> GetTaskByIdAsync(int taskId);

        TaskDto GetTaskById(int taskId);

        void DeleteTask(int taskId);

        IList<TaskDto> GetAllTasks();

        PagedResultDto<TaskDto> GetPagedTasks(GetTaskInput input);

        TaskCacheItem GetTaskFromCacheById(int taskId);
    }
}
