using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Api.Dtos;
using TaskManagement.Model;

namespace TaskManagement.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {            
            CreateMap<UserTask, TaskDto>().ReverseMap();
            CreateMap<UserTask, TaskForListDto>().ForMember(dest=>dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
        }
    }
}
