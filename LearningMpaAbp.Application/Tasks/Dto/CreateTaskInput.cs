using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningMpaAbp.Tasks.MyTask;

namespace LearningMpaAbp.Tasks.Dto
{
    [AutoMapTo(typeof(MyTask))]
    public class CreateTaskInput
    {
        public int? AssignedPersonId { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public TaskState State { get; set; }


        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId={0},Title={1},Description={2}]",
                AssignedPersonId, Title, Description);
        }

    }
}
