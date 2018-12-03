using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningMpaAbp.Tasks.MyTask;

namespace LearningMpaAbp.Tasks.Dto
{
    [AutoMapTo(typeof(MyTask))]
    public class GetTaskInput
    {
        public TaskState? State { get; set; }

        public int? AssingedPersonId { get; set; }
    }
}
