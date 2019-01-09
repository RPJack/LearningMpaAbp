using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Notifications;
using Abp.Threading;
using Abp.Timing;
using AutoMapper;
using Castle.Core.Internal;
using LearningMpaAbp.Authorization;
using LearningMpaAbp.Authorization.Users;
using LearningMpaAbp.Tasks.Dto;

namespace LearningMpaAbp.Tasks
{
    public class TaskAppServices : LearningMpaAbpAppServiceBase, ITaskAppServices
    {
        private readonly IRepository<MyTask> taskRespository;
        private readonly IRepository<User,long> userRepository;
        private readonly INotificationPublisher notificationPublisher;
        private readonly ITaskCache taskCache;

        public TaskAppServices(IRepository<MyTask> taskRespository,IRepository<User,long> userRepository, INotificationPublisher notificationPublisher,ITaskCache taskCache)
        {
            this.taskRespository = taskRespository;
            this.userRepository = userRepository;
            this.notificationPublisher = notificationPublisher;
            this.taskCache = taskCache;
        }

        public int CreateTask(CreateTaskInput input)
        {
            Logger.Info("Creating a task for input:"+input);

            //获取当前用户
            var currentUser = AsyncHelper.RunSync(this.GetCurrentUserAsync);
            //判断用户是否有权限
            if (input.AssignedPersonId.HasValue && input.AssignedPersonId.Value != currentUser.Id)
                PermissionChecker.Authorize(PermissionNames.Pages_Tasks_AssignPerson);

            var task = Mapper.Map<MyTask>(input);
            int result = taskRespository.InsertAndGetId(task);
            //只有创建成功才发送邮件通知
            if (result > 0)
            {
                task.CreationTime = Clock.Now;
                if (task.AssignedPersonId.HasValue)
                {
                    task.AssignedPerson = userRepository.Load(input.AssignedPersonId.Value);
                    var message = "You have been assigned one task into your todo list.";

                    notificationPublisher.Publish("NewTask",new MessageNotificationData(message),null,NotificationSeverity.Info,new[] { task.AssignedPerson.ToUserIdentifier()});
                }
            }
            return result;
            //var task = new MyTask
            //{
            //    Title = input.Title,
            //    Description = input.Description,
            //    State = input.State,
            //    CreationTime = Clock.Now
            //};
            //return taskRespository.InsertAndGetId(task);
        }

        [AbpAuthorize(PermissionNames.Pages_Tasks_Delete)]
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

        public TaskCacheItem GetTaskFromCacheById(int taskId)
        {
            return taskCache[taskId];
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

            //获取当前用户
            var currentUser = AsyncHelper.RunSync(this.GetCurrentUserAsync);
            //判断用户是否有权限
            bool canAssignTaskToOther = PermissionChecker.IsGranted(PermissionNames.Pages_Tasks_AssignPerson);
            //如果任务已经分配且未分配给自己，且不具有分配任务权限，则抛出异常
            if (input.AssignedPersonId.HasValue && input.AssignedPersonId.Value != currentUser.Id && !canAssignTaskToOther)
                throw new AbpAuthorizationException("没有分配任务给他人的权限");

            var task = Mapper.Map<MyTask>(input);//taskRespository.Get(input.Id);
            taskRespository.Update(task);
            //if (input.State.HasValue)
            //{
            //    task.State = input.State.Value;
            //}
            //此处不用做保存操作
            //因为应用程序服务方法是“工作单位”范围作为默认值
            //ABP在“工作单元”范围结束时（没有任何例外）自动保存所有更改
        }
    }
}
