using Abp.Web.Models;
using LearningMpaAbp.Tasks;
using LearningMpaAbp.Tasks.Dto;
using LearningMpaAbp.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static LearningMpaAbp.Tasks.MyTask;

namespace LearningMpaAbp.Web.Controllers
{
    public class BackendTaskController : Controller
    {
        private readonly ITaskAppServices taskAppServices;
        private readonly IUserAppService userAppService;

        public BackendTaskController(ITaskAppServices taskAppServices, IUserAppService userAppService)
        {
            this.taskAppServices = taskAppServices;
            this.userAppService = userAppService;
        }
        // GET: BackendTask
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            ViewBag.TaskStateDropdownList = GetTaskStateDropdownList(null);
            var userList = userAppService.GetUsers();
            ViewBag.AssignedPersonId = new SelectList(userList.Items, "Id", "Name");
            return View();
        }

        /// <summary>
        /// Task-后台管理
        /// </summary>
        /// <param name="limit">分页参数，指定每页最多显示多少行</param>
        /// <param name="offset">分页参数，指定偏移量</param>
        /// <param name="sortfiled">排序参数，排序字段</param>
        /// <param name="sortway">排序参数，排序方式（升序or降序）</param>
        /// <param name="search">过滤参数，指定过滤的任务名称</param>
        /// <param name="status">过滤参数，指定过滤的任务状态</param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult GetAllTasks(int limit, int offset, string sortfiled, string sortway, string search, string status)
        {
            var sort = !string.IsNullOrEmpty(sortfiled) ? string.Format("{0},{1}", sortfiled, sortway) : "";
            TaskState currentState;
            //if(!string.IsNullOrEmpty(status)) Enum.TryParse(status,true,out currentState);

            var filter = new GetTaskInput
            {
                SkipCount = offset,
                MaxResultCount = limit,
                Sorting = sort,
                Filter = search
            };

            if (!string.IsNullOrEmpty(status)) if (Enum.TryParse(status, true, out currentState)) filter.State = currentState;

            var pageTasks = taskAppServices.GetPagedTasks(filter);

            return Json(new { total = pageTasks.TotalCount, rows = pageTasks.Items }, JsonRequestBehavior.AllowGet);
        }



        private List<SelectListItem> GetTaskStateDropdownList(TaskState? curState)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "AllTasks",
                Value = "",
                Selected=curState==null
                }
            };
            list.AddRange(Enum.GetValues(typeof(TaskState)).Cast<TaskState>().Select(s => new SelectListItem
            {
                Text = $"TaskState_{s}",
                Value = s.ToString(),
                Selected = s == curState
            }));

            return list;
        }

    }
}