using LearningMpaAbp.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tests.Tasks
{
    public class MyTaskAppService_Tests:LearningMpaAbpTestBase
    {
        private readonly ITaskAppServices myTaskAppServices;

        public MyTaskAppService_Tests()
        {
            myTaskAppServices = Resolve<TaskAppServices>();
        }
    }
}
