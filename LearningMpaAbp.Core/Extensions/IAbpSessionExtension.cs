using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Extensions
{
    public interface IAbpSessionExtension:IAbpSession
    {
        string Email { get; }
    }
}
