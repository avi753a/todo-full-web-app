using AutoMapper;
using System.Reflection;
using TodoApp.Data.Entites;
using TodoApp.Models;
namespace TodoApp.Service
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            ConfigureMappers();

        }
        public void ConfigureMappers()
        {
            CreateMap<TaskItem, TaskView>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
           .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
           .ForMember(dest => dest.EditedTime, opt => opt.MapFrom(src => src.EditedTime))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
           .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name)) 
           .ForMember(dest => dest.PriorityValue, opt => opt.MapFrom(src => src.Priority.Value));

            CreateMap<TaskDTO, TaskItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => DateTime.UtcNow.ToUniversalTime()))
                .ForMember(dest => dest.EditedTime, opt => opt.MapFrom(src => DateTime.UtcNow.ToUniversalTime()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.PriorityId, opt => opt.MapFrom(src=>src.PriorityId));



            CreateMap<Priority, PriorityView>().ReverseMap();

            CreateMap<Status,StatusView>().ReverseMap();

            CreateMap<StatusDTO, Status>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src=>0));

            CreateMap<PriorityDTO, Priority>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));

            CreateMap<StatusUpdation, Status>().ReverseMap();

            CreateMap<PriorityUpdation, Priority>().ReverseMap();

            CreateMap<TaskItem, TaskDetails>().ReverseMap();


        }
    }
}
