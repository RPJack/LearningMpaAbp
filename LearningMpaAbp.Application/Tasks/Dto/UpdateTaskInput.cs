using Abp.AutoMapper;
using Abp.Runtime.Validation;
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
    public class UpdateTaskInput : ICustomValidate//自定义验证接口
    {
        [Range(1,Int32.MaxValue)]
        public int Id { get; set; }


        public int? AssignedPersonId { get; set; }


        public TaskState? State { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 自定义验证方法，在数据验证后由ABP调用
        /// </summary>
        /// <param name="context"></param>
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (AssignedPersonId == null && State == null)
            {
                context.Results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Task",new[] { "AssignedPersonId", "State"}));
            }
        }


        public override string ToString()
        {
            return string.Format("[UpdateTaskInput>TaskId={0},AssignedPersonId={1},State={2}]",
                Id, AssignedPersonId, State);
        }

    }
}
