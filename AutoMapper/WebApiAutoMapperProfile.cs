using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.AutoMapper
{
    public class WebApiAutoMapperProfile : Profile
    {
        public WebApiAutoMapperProfile()
        {
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<Meeting, MeetingDto>().ReverseMap();
            CreateMap<MemberMeeting, MemberMeetingDto>().ReverseMap();
            CreateMap<MemberModel, MemberModelDto>().ReverseMap();
            CreateMap<MemberTraining, MemberTrainingDto>().ReverseMap();
            CreateMap<Training, TrainingDto>().ReverseMap();
            CreateMap<Responsibility, ResponsabilityDto>().ReverseMap();
        }
    }
}
