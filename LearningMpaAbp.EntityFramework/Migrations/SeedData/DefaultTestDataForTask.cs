using LearningMpaAbp.EntityFramework;
using LearningMpaAbp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Migrations.SeedData
{
    public class DefaultTestDataForTask
    {
        private readonly LearningMpaAbpDbContext context;

        private static readonly List<MyTask> tasks;

        public DefaultTestDataForTask(LearningMpaAbpDbContext context)
        {
            this.context = context;
        }

        static DefaultTestDataForTask()
        {
            tasks = new List<MyTask>() {
                new MyTask("Learning ABP Demo", "How to learning ABP"),
                new MyTask( "Learning ABP Demo2", "How to learning ABP(Mpa)")
            };
        }


        public void Create()
        {
            foreach (var item in tasks)
            {
                if (context.Tasks.FirstOrDefault(t => t.Title == item.Title) == null)
                {
                    context.Tasks.Add(item);
                }
                context.SaveChanges();
            }
        }


    }
}
