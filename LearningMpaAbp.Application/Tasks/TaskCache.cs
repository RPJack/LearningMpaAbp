using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Entities.Caching;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using LearningMpaAbp.Tasks.Dto;

namespace LearningMpaAbp.Tasks
{
    public class TaskCache : EntityCache<MyTask, TaskCacheItem>, ITaskCache, ISingletonDependency
    {
        public TaskCache(ICacheManager cacheManager, IRepository<MyTask, int> repository, string cacheName = null) : base(cacheManager, repository, cacheName)
        {
        }
    }
}
