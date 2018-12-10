using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Timing;
using AutoMapper;
using Castle.Core.Internal;
using LearningMpaAbp.Tasks.Dto;

namespace LearningMpaAbp.Tasks
{
    public class TaskAppServices : LearningMpaAbpAppServiceBase, ITaskAppServices
    {
        private readonly IRepository<MyTask> taskRespository;

        public TaskAppServices(IRepository<MyTask> taskRespository)
        {
            this.taskRespository = taskRespository;
        }

        public int CreateTask(CreateTaskInput input)
        {
            Logger.Info("Creating a task for input:"+input);
            var task = new MyTask
            {
                Title = input.Title,
                Description = input.Description,
                State = input.State,
                CreationTime = Clock.Now
            };
            return taskRespository.InsertAndGetId(task);
        }

        public void DeleteTask(int taskId)
        {
            var task = taskRespository.Get(taskId);
            if (task != null)
            {
                taskRespository.Delete(task);
            }
        }

        public IList<TaskDto> GetAllTasks()
        {
            var tasks = taskRespository.GetAll();
            return tasks.MapTo<List<TaskDto>>();
        }

        /// <summary>
        /// 任务分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<TaskDto> GetPagedTasks(GetTaskInput input)
        {
            //过滤
            var query = taskRespository.GetAll()
                .WhereIf(input.State.HasValue, t => t.State == input.State.Value)
                .WhereIf(!input.Filter.IsNullOrEmpty(), t => t.Title.Contains(input.Filter))
                .WhereIf(input.AssingedPersonId.HasValue, t => t.AssignedPersonId == input.AssingedPersonId.Value);

            //排序
            query = !string.IsNullOrEmpty(input.Sorting) ? query.OrderBy(input.Sorting.ToString()) : query.OrderByDescending(t => t.CreationTime);

            //获取总数
            var taskCount = query.Count();

            //分页方式-以前的分页方式
            //var taskList = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            //ABP自带分页方式
            var taskList = query.PageBy(input).ToList();

            return new PagedResultDto<TaskDto>(taskCount,taskList.MapTo<List<TaskDto>>());
        }

        public TaskDto GetTaskById(int taskId)
        {
            var task = taskRespository.Get(taskId);
            return task.MapTo<TaskDto>();
        }

        public async Task<TaskDto> GetTaskByIdAsync(int taskId)
        {
            var task = await taskRespository.GetAsync(taskId);
            return task.MapTo<TaskDto>();
        }

        public GetTasksOutput GetTasks(GetTaskInput input)
        {
            var query = taskRespository.GetAll();
            if (input.AssingedPersonId.HasValue)
            {
                query = query.Where(t => t.AssignedPersonId == input.AssingedPersonId.Value);
            }
            if (input.State.HasValue)
            {
                query = query.Where(t => t.State == input.State.Value);
            }

            return new GetTasksOutput()
            {
                Tasks = Mapper.Map<List<TaskDto>>(query.ToList())
            };
        }



        public void UpdateTask(UpdateTaskInput input)
        {
            Logger.Info("Updating a task for input:"+input);
            var task = taskRespository.Get(input.Id);
            if (input.State.HasValue)
            {
                task.State = input.State.Value;
            }
            //此处不用做保存操作
            //因为应用程序服务方法是“工作单位”范围作为默认值
            //ABP在“工作单元”范围结束时（没有任何例外）自动保存所有更改
        }
    }
}
