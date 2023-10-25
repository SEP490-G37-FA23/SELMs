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
                cfg.CreateMap<UserDTO, User>().ReverseMap();

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