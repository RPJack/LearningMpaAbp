using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using LearningMpaAbp.Authorization.Roles;
using LearningMpaAbp.Authorization.Users;
using LearningMpaAbp.Roles.Dto;
using LearningMpaAbp.Tasks;
using LearningMpaAbp.Tasks.Dto;
using LearningMpaAbp.Users.Dto;

namespace LearningMpaAbp
{
    [DependsOn(typeof(LearningMpaAbpCoreModule), typeof(AbpAutoMapperModule))]
    public class LearningMpaAbpApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(mapper =>
            {
                //Add your custom AutoMapper mappings here...
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());


                cfg.CreateMap<CreateTaskInput, MyTask>();
                cfg.CreateMap<UpdateTaskInput, MyTask>();
                cfg.CreateMap<TaskDto, UpdateTaskInput>();
                cfg.CreateMap<MyTask, TaskDto>().ForMember(dto=>dto.AssignedPersonName,map=>map.MapFrom(m=>m.AssignedPerson.FullName));

                //var mappers = IocManager.IocContainer.ResolveAll<IDtoMapping>();
                //foreach (var item in mappers)
                //{
                //    item.CreateMapping(mapper);
                //}

            });
        }
    }
}
