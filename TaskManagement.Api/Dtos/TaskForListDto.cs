﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Api.Dtos
{
    public class TaskForListDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }   
        
        public string Description { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
        
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
