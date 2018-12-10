using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Dto
{
    public class PagedAndFilteredInputDto : IPagedResultRequest
    {
        [Range(0,int.MaxValue)]
        public int SkipCount { get; set; }

        [Range(1,AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }


    }
}
