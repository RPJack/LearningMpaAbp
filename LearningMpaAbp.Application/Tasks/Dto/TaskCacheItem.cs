using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningMpaAbp.Tasks.MyTask;

namespace LearningMpaAbp.Tasks.Dto
{
    [AutoMapFrom(typeof(MyTask))]
    public class TaskCacheItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
    }
}
