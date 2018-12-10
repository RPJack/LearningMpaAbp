using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LearningMpaAbp.Tasks.MyTask;

namespace LearningMpaAbp.Tasks.Dto
{
    [AutoMap(typeof(MyTask))]
    public class TaskDto:EntityDto
    {

        public long? AssignedPersonId { get; set; }

        public string AssignedPersonName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonConverter(typeof(DateFormat))]//序列化时间（return Json时使用）
        public DateTime CreationTime { get; set; }

        public TaskState State { get; set; }

        public override string ToString()
        {
            return string.Format("[Task Id = {0},Description={1},CreationTime={2},AssignedPersonName={3},State={4}]",
                Id, Description, CreationTime, AssignedPersonName, State);
        }




    }
}
