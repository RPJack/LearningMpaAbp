﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using LearningMpaAbp.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningMpaAbp.Tasks
{
    public class MyTask : Entity, IHasCreationTime
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024;//64kb

        public long? AssignedPersonId { get; set; }

        [ForeignKey("AssignedPersonId")]
        public User AssignedPerson { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public TaskState State { get; set; }
        public DateTime CreationTime { get; set; }

        public MyTask()
        {
            CreationTime = Clock.Now;
            State = TaskState.Open;
        }

        public MyTask(string title,string description = null):this()
        {
            Title = title;
            Description = description;
        }

        public enum TaskState:byte
        {
            Open = 0,
            Completed = 1
        }



    }
}
