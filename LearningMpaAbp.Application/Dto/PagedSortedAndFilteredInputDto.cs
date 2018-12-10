using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Dto
{
    public class PagedSortedAndFilteredInputDto:PagedAndSortedInputDto
    {
        public string Filter { get; set; }
    }
}
