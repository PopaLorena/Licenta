using AutoMapper;
using Licenta.Dto;
using Licenta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Controllers.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Meeting, MeetingDto>();
            CreateMap<MemberModel, MemberModelDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Training, TrainingDto>();
            CreateMap<Responsibility, ResponsabilityDto>();
            CreateMap<MemberMeeting, MemberMeetingDto>();
            CreateMap<MemberTraining, MemberTrainingDto>();
        }
    }
}
