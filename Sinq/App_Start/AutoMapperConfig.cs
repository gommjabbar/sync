using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Sinq.DTO;
using Sinq.Models;

namespace Sinq.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure() {

            Mapper.CreateMap<ActivityDTO, Activity>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Completed, opt => opt.MapFrom(src => src.Completed))
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.ElapsedTime, opt => opt.MapFrom(src => src.ActivityTimes.Sum(activityTime => (activityTime.EndDate - activityTime.StartDate).TotalSeconds))) 
            .ForMember(x => x.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(x => x.IsStarted, opt => opt.MapFrom(src => src.IsStarted)); 


            Mapper.CreateMap<Activity, ActivityDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Completed, opt => opt.MapFrom(src => src.Completed))
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.ActivityTimes.Sum(activityTime => (activityTime.EndDate - activityTime.StartDate).TotalSeconds), opt=>opt.MapFrom(src=>src.ElaspedTime))
            .ForMember(x => x.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(x => x.IsStarted, opt => opt.MapFrom(src => src.IsStarted)); 
        }
    }
}