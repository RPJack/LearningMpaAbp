using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tasks
{
    /// <summary>
    /// 自定义仓储
    /// </summary>
    public interface ITaskAppServices:IRepository<MyTask>
    {
        List<MyTask> GetTaskByAssignedPersonId(long personId);
    }
}
