using Abp.Domain.Repositories;
using LearningMpaAbp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.IRespositories
{
    /// <summary>
    /// 自定义仓储Demo
    /// </summary>
    public interface IBackedTaskRespository:IRepository<MyTask>
    {
        /// <summary>
        /// 获取某个用户分配了哪些任务
        /// </summary>
        List<MyTask> GetTaskByAssignedPersonId(long personId);
    }
}
