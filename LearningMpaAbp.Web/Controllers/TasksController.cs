using Abp.Application.Services.Dto;
using Abp.Runtime.Caching;
using Abp.Web.Mvc.Authorization;
using LearningMpaAbp.Tasks;
using LearningMpaAbp.Tasks.Dto;
using LearningMpaAbp.Users;
using LearningMpaAbp.Users.Dto;
using LearningMpaAbp.Web.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace LearningMpaAbp.Web.Controllers
{
    [AbpMvcAuthorize]//权限认证
    public class TasksController : LearningMpaAbpControllerBase
    {
        private readonly ITaskAppServices _taskAppServices;
        private readonly IUserAppService _userAppService;
        private readonly ICacheManager _cachManager;

        public TasksController(ITaskAppServices taskAppServices,IUserAppService userAppService,ICacheManager cacheManager)
        {
            _taskAppServices = taskAppServices;
            _userAppService = userAppService;
            _cachManager = cacheManager;

        }
        // GET: Tasks
        public ActionResult Index(GetTaskInput input)
        {
            var output = _taskAppServices.GetTasks(input);
            var model = new IndexViewModel(output.Tasks)
            {
                SelectedTaskState = input.State
            };
            return View(model);
        }


        public PartialViewResult RemoteCreate()
        {
            var userList = _cachManager.GetCache("Controller").Get("AllUsers",
                ()=> _userAppService.GetUsers() as ListResultDto<UserDto>);
            ViewBag.AssignedPersonId = new SelectList(userList.Items, "Id", "Name");
            return PartialView("_CreateTaskPartial");
        }

        [OutputCache(Duration =120,VaryByCustom ="none")]
        [ChildActionOnly]
        public PartialViewResult Create()
        {
            var users = _userAppService.GetAll();
            ViewBag.AssignedPersonId = new SelectList(users,"Id","Name" );
            return PartialView("_CreateTask");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateTaskInput task)
        {
            var id = _taskAppServices.CreateTask(task);
            var input = new GetTaskInput();
            var output = _taskAppServices.GetTasks(input);
            return PartialView("_List", output.Tasks);
        }

        public PartialViewResult GetList(GetTaskInput input)
        {
            var output = _taskAppServices.GetTasks(input);
            return PartialView("_List",output.Tasks);
        }

        public ActionResult PagedList(int? page)
        {
            var pageSize = 5;
            var pageNumber = page ?? 1;
            var filter = new GetTaskInput
            {
                SkipCount = (pageNumber - 1) * pageSize,//忽略个数
                MaxResultCount = pageSize
            };
            var result = _taskAppServices.GetPagedTasks(filter);
            //已在应用服务层手动完成了分页逻辑，所以需要手动构造分页结果
            var onePageOfTasks = new StaticPagedList<TaskDto>(result.Items, pageNumber, pageSize, result.TotalCount);
            //将分页结果放入到ViewBag供View使用
            ViewBag.OnePageOfTasks = onePageOfTasks;
            return View();
        }

        
        public PartialViewResult Edit(int id)
        {
            var task = _taskAppServices.GetTaskById(id);

            var updateTaskDto = AutoMapper.Mapper.Map<UpdateTaskInput>(task);

            var userList = _userAppService.GetUsers();
            ViewBag.AssignedPersonId = new SelectList(userList.Items, "Id", "Name", updateTaskDto.AssignedPersonId);

            return PartialView("_EditTask", updateTaskDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateTaskInput updateTaskDto)
        {
            _taskAppServices.UpdateTask(updateTaskDto);

            var input = new GetTaskInput();
            var output = _taskAppServices.GetTasks(input);

            return PartialView("_List", output.Tasks);
        }


        public void DeleteTask(int TaskId)
        {
            _taskAppServices.DeleteTask(TaskId);
        }

        
        public JsonResult adl()
        {
            var aa = new object();

            return AbpJson(aa,null,null,JsonRequestBehavior.AllowGet);
        }


    }
}