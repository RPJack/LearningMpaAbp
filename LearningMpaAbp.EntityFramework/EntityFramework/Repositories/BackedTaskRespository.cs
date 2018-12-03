using Abp.EntityFramework;
using LearningMpaAbp.IRespositories;
using LearningMpaAbp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.EntityFramework.Repositories
{
    public class BackedTaskRespository: LearningMpaAbpRepositoryBase<MyTask>,IBackedTaskRespository
    {
        protected BackedTaskRespository(IDbContextProvider<LearningMpaAbpDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<MyTask> GetTaskByAssignedPersonId(long personId)
        {
            var query = GetAll();
            if (personId > 0)
            {
                query = query.Where(t=>t.AssignedPersonId==personId);
            }
            return query.ToList();
        }
    }
}
