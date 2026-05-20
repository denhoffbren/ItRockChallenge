using AutoMapper;
using TaskService.Application.DTOs;
using TaskService.Domain.Entities;

namespace TaskService.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedTaskDto, Tarea>().ReverseMap();
            CreateMap<TaskResponseDto, Tarea>().ReverseMap();
            CreateMap<UpdatedTaskDto, Tarea>().ReverseMap();
        }
    }
}
