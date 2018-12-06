﻿using Abp.Web.Mvc.Authorization;
using LearningMpaAbp.Tasks;
using LearningMpaAbp.Tasks.Dto;
using LearningMpaAbp.Users;
using LearningMpaAbp.Web.Models.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningMpaAbp.Web.Controllers
{
    [AbpMvcAuthorize]//权限认证
    public class TasksController : LearningMpaAbpControllerBase
    {
        private readonly ITaskAppServices _taskAppServices;
        private readonly IUserAppService _userAppService;

        public TasksController(ITaskAppServices taskAppServices,IUserAppService userAppService)
        {
            _taskAppServices = taskAppServices;
            _userAppService = userAppService;
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
        

    }
}