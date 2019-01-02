using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;

namespace LearningMpaAbp.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(LearningMpaAbpApplicationModule))]
    public class LearningMpaAbpWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            //将当前程序集的特定类或接口注册到依赖注入容器中
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(LearningMpaAbpApplicationModule).Assembly, "app")
                .Build();

            Configuration.Modules.AbpWebApi().HttpConfiguration.Filters.Add(new HostAuthenticationFilter("Bearer"));
        }
    }
}
