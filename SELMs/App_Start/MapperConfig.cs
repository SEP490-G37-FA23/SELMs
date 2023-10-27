using AutoMapper;
using SELMs.Api.DTOs;
using SELMs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SELMs.App_Start
{
    public static class MapperConfig
    {
        public static Mapper Initialize()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {
                //Configuring Employee and EmployeeDTO
                cfg.CreateMap<UserDTO, User>()
                .ForMember(des => des.date_of_birth, act => act.MapFrom(src => DateTime.Parse(src.date_of_birth)))
                .ForMember(des => des.hire_date, act => act.MapFrom(src => DateTime.Parse(src.hire_date)))
                .ForMember(des => des.resignation_date, act => act.MapFrom(src => DateTime.Parse(src.resignation_date)));

                cfg.CreateMap<User, UserDTO>()
                .ForMember(des => des.date_of_birth, act => act.MapFrom(src => src.date_of_birth.ToString()))
                .ForMember(des => des.hire_date, act => act.MapFrom(src => src.hire_date.ToString()))
                .ForMember(des => des.resignation_date, act => act.MapFrom(src => src.resignation_date.ToString()));

                //Configuring Category and CategoryDTO
                cfg.CreateMap<CategoryDTO, Category>().ReverseMap();

                //Configuring Equipment and EquipmentDTO
                cfg.CreateMap<EquipmentDTO, Equipment>().ReverseMap();
                cfg.CreateMap<List<EquipmentDTO>, List<Equipment>>().ReverseMap();

                //Configuring Equipment and EquipmentDTO
                cfg.CreateMap<ImageDTO, Image>().ReverseMap();
                cfg.CreateMap<List<ImageDTO>, List<Image>>().ReverseMap();

                //Any Other Mapping Configuration ....
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}