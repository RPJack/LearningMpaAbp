using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp
{
    /// <summary>
    /// 如果在映射规则既有通过特性方式又有通过代码方式创建，这时就会容易混乱不便维护。
    /// 为了解决这个问题，统一采用代码创建映射规则的方式。并通过IOC容器注册所有的映射规则类，再循环调用注册方法
    /// 实现该接口以进行映射规则创建
    /// </summary>
    public interface IDtoMapping
    {
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
