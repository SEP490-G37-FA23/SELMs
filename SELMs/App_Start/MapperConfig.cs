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
                //Configuring User and UserDTO
                cfg.CreateMap<UserDTO, User>()
                .ForMember(des => des.date_of_birth, act => act.MapFrom(src => DateTime.Parse(src.date_of_birth)))
                .ForMember(des => des.hire_date, act => act.MapFrom(src => DateTime.Parse(src.hire_date)))
                .ForMember(des => des.resignation_date, act => act.MapFrom(src => DateTime.Parse(src.resignation_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<User, UserDTO>()
                .ForMember(des => des.date_of_birth, act => act.MapFrom(src => src.date_of_birth.ToString()))
                .ForMember(des => des.hire_date, act => act.MapFrom(src => src.hire_date.ToString()))
                .ForMember(des => des.resignation_date, act => act.MapFrom(src => src.resignation_date.ToString()));

                //Configuring Category and CategoryDTO
                cfg.CreateMap<CategoryDTO, Category>().ReverseMap();

                //Configuring Equipment and EquipmentDTO
                cfg.CreateMap<EquipmentDTO, Equipment>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => DateTime.Parse(src.create_date)));

                cfg.CreateMap<Equipment, EquipmentDTO>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => src.create_date.ToString()));

                //Configuring Image and ImageDTO
                cfg.CreateMap<ImageDTO, Image>().ReverseMap();
                cfg.CreateMap<List<ImageDTO>, List<Image>>().ReverseMap();

                //Configuring Project and ProjectDTO
                cfg.CreateMap<ProjectDTO, Project>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => DateTime.Parse(src.create_date)))
                .ForMember(des => des.start_date, act => act.MapFrom(src => DateTime.Parse(src.start_date)))
                .ForMember(des => des.end_date, act => act.MapFrom(src => DateTime.Parse(src.end_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Project, ProjectDTO>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => src.create_date.ToString()))
                .ForMember(des => des.start_date, act => act.MapFrom(src => src.start_date.ToString()))
                .ForMember(des => des.end_date, act => act.MapFrom(src => src.end_date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Location and LocationDTO
                cfg.CreateMap<LocationDTO, Location>().ReverseMap();
                cfg.CreateMap<List<LocationDTO>, List<Location>>().ReverseMap();

                //Configuring EquipmentAllocationApplication and EquipmentAllocationApplicationDTO
                cfg.CreateMap<EquipmentAllocationApplicationDTO, Equipment_Allocation_Application>()
                .ForMember(des => des.application_date, act => act.MapFrom(src => DateTime.Parse(src.application_date)))
                .ForMember(des => des.approved_date, act => act.MapFrom(src => DateTime.Parse(src.approved_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Equipment_Allocation_Application, EquipmentAllocationApplicationDTO>()
                .ForMember(des => des.application_date, act => act.MapFrom(src => src.application_date.ToString()))
                .ForMember(des => des.approved_date, act => act.MapFrom(src => src.approved_date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring EquipmentAllocationApplicationDetail and EquipmentAllocationApplicationDetailDTO
                cfg.CreateMap<EquipmentAllocationApplicationDetailDTO, Equipment_Allocation_Application_Detail>().ReverseMap();

                //Configuring EquipmentImportApplication and EquipmentImportApplicationDTO
                cfg.CreateMap<EquipmentImportApplicationDTO, Equipment_Import_Application>()
                .ForMember(des => des.application_date, act => act.MapFrom(src => DateTime.Parse(src.application_date)))
                .ForMember(des => des.approved_date, act => act.MapFrom(src => DateTime.Parse(src.approved_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Equipment_Import_Application, EquipmentImportApplicationDTO>()
                .ForMember(des => des.application_date, act => act.MapFrom(src => src.application_date.ToString()))
                .ForMember(des => des.approved_date, act => act.MapFrom(src => src.approved_date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Equipment_Import_Application_Detail and EquipmentImportApplicationDetailDTO
                cfg.CreateMap<EquipmentImportApplicationDetailDTO, Equipment_Import_Application_Detail>().ReverseMap();




                //Any Other Mapping Configuration ....
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}