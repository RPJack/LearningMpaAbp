using Abp.Authorization;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Authorization
{
    public class TaskAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }
            var tasks = pages.CreateChildPermission(PermissionNames.Pages_Tasks, L("Tasks"));
            tasks.CreateChildPermission(PermissionNames.Pages_Tasks_AssignPerson, L("AssignTaskToPerson"));
            tasks.CreateChildPermission(PermissionNames.Pages_Tasks_Delete, L("DeleteTask"));
            //return new LocalizableString(context, LearningMpaAbpConsts.LocalizationSourceName);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, LearningMpaAbpConsts.LocalizationSourceName);
        }

    }
}
