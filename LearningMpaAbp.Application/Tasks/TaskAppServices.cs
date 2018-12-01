using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tasks
{
    public class TaskAppServices : ITaskAppServices
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Delete(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public MyTask FirstOrDefault(int id)
        {
            throw new NotImplementedException();
        }

        public MyTask FirstOrDefault(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> FirstOrDefaultAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> FirstOrDefaultAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public MyTask Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<MyTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<MyTask> GetAllIncluding(params Expression<Func<MyTask, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public List<MyTask> GetAllList()
        {
            throw new NotImplementedException();
        }

        public List<MyTask> GetAllList(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<MyTask>> GetAllListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<MyTask>> GetAllListAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<MyTask> GetTaskByAssignedPersonId(long personId)
        {
            throw new NotImplementedException();
        }

        public MyTask Insert(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public int InsertAndGetId(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertAndGetIdAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> InsertAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public MyTask InsertOrUpdate(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public int InsertOrUpdateAndGetId(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> InsertOrUpdateAndGetIdAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> InsertOrUpdateAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public MyTask Load(int id)
        {
            throw new NotImplementedException();
        }

        public long LongCount()
        {
            throw new NotImplementedException();
        }

        public long LongCount(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<long> LongCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<long> LongCountAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T Query<T>(Func<IQueryable<MyTask>, T> queryMethod)
        {
            throw new NotImplementedException();
        }

        public MyTask Single(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> SingleAsync(Expression<Func<MyTask, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public MyTask Update(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public MyTask Update(int id, Action<MyTask> updateAction)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> UpdateAsync(MyTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<MyTask> UpdateAsync(int id, Func<MyTask, Task> updateAction)
        {
            throw new NotImplementedException();
        }
    }
}
