using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LearningMpaAbp.Tasks.Dto;

namespace LearningMpaAbp.Tasks
{
    public class TaskDtoMapping : IDtoMapping
    {
        public void CreateMapping(IMapperConfigurationExpression mapperConfig)
        {
            //定义单向映射
            mapperConfig.CreateMap<CreateTaskInput, MyTask>();
            mapperConfig.CreateMap<UpdateTaskInput,MyTask>();
            mapperConfig.CreateMap<TaskDto, UpdateTaskInput>();


            //自定义映射
            var taskDtoMapper = mapperConfig.CreateMap<MyTask, TaskDto>();
            taskDtoMapper.ForMember(dto => dto.AssignedPersonName, map => map.MapFrom(m => m.AssignedPerson.FullName));

        }
    }
}
