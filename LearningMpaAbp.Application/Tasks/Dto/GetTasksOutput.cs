using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tasks.Dto
{
    [AutoMapFrom(typeof(MyTask))]
    public class GetTasksOutput
    {
        public List<TaskDto> Tasks { get; set; }
    }
}
