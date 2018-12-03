using LearningMpaAbp.EntityFramework;
using LearningMpaAbp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Migrations.SeedData
{
    public class DefaultTestDataForMyTask
    {
        private readonly LearningMpaAbpDbContext context;
        private static readonly List<MyTask> tasks;


        public DefaultTestDataForMyTask(LearningMpaAbpDbContext context)
        {
            this.context = context;
        }

        static DefaultTestDataForMyTask()
        {
            tasks = new List<MyTask>()
            {
                new MyTask("ABP Demo","How to Learning ABP"),
                new MyTask("ABP Demo2","Where to Learning ABP"),
            };
        }

        public void Create()
        {
            foreach (var task in tasks)
            {
                if (context.myTask.FirstOrDefault(t => t.Title == task.Title) == null)
                {
                    context.myTask.Add(task);
                }
                context.SaveChanges();
            }
        }


    }
}
