using System;
using System.Collections.Generic;
using AutoMapper;
using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Models.BusinessModel;

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
                cfg.CreateMap<ImageDTO, Image>()
                .ForMember(des => des.date, act => act.MapFrom(src => DateTime.Parse(src.date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Image, ImageDTO>()
                .ForMember(des => des.date, act => act.MapFrom(src => src.date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Project and ProjectDTO
                cfg.CreateMap<ProjectDTO, Project>()
              
                .ForMember(des => des.start_date, act => act.MapFrom(src => HandleDateTime.formatDate(src.start_date)))
                .ForMember(des => des.end_date, act => act.MapFrom(src => HandleDateTime.formatDate(src.end_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Project, ProjectDTO>()
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

                //Configuring Equipment_Handover_Form and EquipmentHandoverFormDTO
                cfg.CreateMap<EquipmentHandoverFormDTO, Equipment_Handover_Form>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => DateTime.Parse(src.create_date)))
                .ForMember(des => des.handover_date, act => act.MapFrom(src => DateTime.Parse(src.handover_date)))
                .ForMember(des => des.receipt_date, act => act.MapFrom(src => DateTime.Parse(src.receipt_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Equipment_Handover_Form, EquipmentHandoverFormDTO>()
                .ForMember(des => des.create_date, act => act.MapFrom(src => src.create_date.ToString()))
                .ForMember(des => des.handover_date, act => act.MapFrom(src => src.handover_date.ToString()))
                .ForMember(des => des.receipt_date, act => act.MapFrom(src => src.receipt_date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Equipment_Handover_Form_Detail and EquipmentHandoverFormDetailDTO
                cfg.CreateMap<EquipmentHandoverFormDetailDTO, Equipment_Handover_Form_Detail>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Equipment_Import_Application_Detail and EquipmentImportApplicationDetailDTO
                cfg.CreateMap<EquipmentImportApplicationDetailDTO, Equipment_Import_Application_Detail>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Equipment_Location_History and EquipmentLocationHistoryDTO
                cfg.CreateMap<EquipmentLocationHistoryDTO, Equipment_Location_History>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Equipment_Project_History and EquipmentProjectHistoryDTO
                cfg.CreateMap<EquipmentProjectHistoryDTO, Equipment_Project_History>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Member_Location_History and MemberLocationHistoryDTO
                cfg.CreateMap<MemberLocationHistoryDTO, Member_Location_History>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Member_Project_History and MemberProjectHistoryDTO
                cfg.CreateMap<MemberProjectHistoryDTO, Member_Project_History>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring Member_Project_History and MemberProjectHistoryDTO
                cfg.CreateMap<AttachmentDTO, Attachment>()
                .ForMember(des => des.date, act => act.MapFrom(src => DateTime.Parse(src.date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Attachment, AttachmentDTO>()
                .ForMember(des => des.date, act => act.MapFrom(src => src.date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring InverntoryRequestApplicationDTO and Inverntory_Request_Application
                cfg.CreateMap<InventoryRequestApplicationDTO, Inventory_Request_Application>().ReverseMap()
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                //Configuring InverntoryRequestApplicationDetailDTO and Inverntory_Request_Application_Detail
                cfg.CreateMap<InventoryRequestApplicationDetailDTO, Inventory_Request_Application_Detail>().ReverseMap()
                .ForMember(des => des.inventory_date, act => act.MapFrom(src => src.inventory_date.ToString()))
                .ForAllOtherMembers(act => act.NullSubstitute(null));

                cfg.CreateMap<Inventory_Request_Application_Detail, InventoryRequestApplicationDetailDTO>().ReverseMap()
                .ForMember(des => des.inventory_date, act => act.MapFrom(src => DateTime.Parse(src.inventory_date)))
                .ForAllOtherMembers(act => act.NullSubstitute(null));



                //Any Other Mapping Configuration ....  
            });

            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}