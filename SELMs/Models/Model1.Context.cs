﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SELMs.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SELMsContext : DbContext
    {
        public SELMsContext()
            : base("name=SELMsContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application_Attachment> Application_Attachment { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Equipment_Allocation_Application> Equipment_Allocation_Application { get; set; }
        public virtual DbSet<Equipment_Allocation_Application_Detail> Equipment_Allocation_Application_Detail { get; set; }
        public virtual DbSet<Equipment_Component> Equipment_Component { get; set; }
        public virtual DbSet<Equipment_Handover_Form> Equipment_Handover_Form { get; set; }
        public virtual DbSet<Equipment_Handover_Form_Detail> Equipment_Handover_Form_Detail { get; set; }
        public virtual DbSet<Equipment_Location_History> Equipment_Location_History { get; set; }
        public virtual DbSet<Equipment_Project_History> Equipment_Project_History { get; set; }
        public virtual DbSet<Equipment_Usage_History> Equipment_Usage_History { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Inventory_Request_Application> Inventory_Request_Application { get; set; }
        public virtual DbSet<Inventory_Request_Application_Detail> Inventory_Request_Application_Detail { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Member_Location_History> Member_Location_History { get; set; }
        public virtual DbSet<Member_Project_History> Member_Project_History { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
    
        public virtual ObjectResult<Proc_GetAllCategoryList_Result> Proc_GetAllCategoryList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetAllCategoryList_Result>("Proc_GetAllCategoryList");
        }
    
        public virtual ObjectResult<Proc_GetAllProjectList_Result> Proc_GetAllProjectList(string username, string project_name, string manager_name, string location_name)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var project_nameParameter = project_name != null ?
                new ObjectParameter("project_name", project_name) :
                new ObjectParameter("project_name", typeof(string));
    
            var manager_nameParameter = manager_name != null ?
                new ObjectParameter("manager_name", manager_name) :
                new ObjectParameter("manager_name", typeof(string));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetAllProjectList_Result>("Proc_GetAllProjectList", usernameParameter, project_nameParameter, manager_nameParameter, location_nameParameter);
        }
    
        public virtual ObjectResult<Proc_GetAllSubLocationList_Result> Proc_GetAllSubLocationList(string username, Nullable<int> parent_id, string location_name)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var parent_idParameter = parent_id.HasValue ?
                new ObjectParameter("parent_id", parent_id) :
                new ObjectParameter("parent_id", typeof(int));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetAllSubLocationList_Result>("Proc_GetAllSubLocationList", usernameParameter, parent_idParameter, location_nameParameter);
        }
    
        public virtual ObjectResult<Proc_GetAvailableEAAList_Result> Proc_GetAvailableEAAList(string username, string project_name, string location_name, string creater_name, string application_code)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var project_nameParameter = project_name != null ?
                new ObjectParameter("project_name", project_name) :
                new ObjectParameter("project_name", typeof(string));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            var creater_nameParameter = creater_name != null ?
                new ObjectParameter("creater_name", creater_name) :
                new ObjectParameter("creater_name", typeof(string));
    
            var application_codeParameter = application_code != null ?
                new ObjectParameter("application_code", application_code) :
                new ObjectParameter("application_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetAvailableEAAList_Result>("Proc_GetAvailableEAAList", usernameParameter, project_nameParameter, location_nameParameter, creater_nameParameter, application_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetCategoryList_Result> Proc_GetCategoryList(Nullable<bool> isadmin, string role, string username, string text)
        {
            var isadminParameter = isadmin.HasValue ?
                new ObjectParameter("isadmin", isadmin) :
                new ObjectParameter("isadmin", typeof(bool));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetCategoryList_Result>("Proc_GetCategoryList", isadminParameter, roleParameter, usernameParameter, textParameter);
        }
    
        public virtual ObjectResult<Proc_GetDetailEquipment_Result> Proc_GetDetailEquipment(string system_code)
        {
            var system_codeParameter = system_code != null ?
                new ObjectParameter("system_code", system_code) :
                new ObjectParameter("system_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetDetailEquipment_Result>("Proc_GetDetailEquipment", system_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetDetailIRAListInLocation_Result> Proc_GetDetailIRAListInLocation(string username, Nullable<int> location_id)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var location_idParameter = location_id.HasValue ?
                new ObjectParameter("location_id", location_id) :
                new ObjectParameter("location_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetDetailIRAListInLocation_Result>("Proc_GetDetailIRAListInLocation", usernameParameter, location_idParameter);
        }
    
        public virtual ObjectResult<Proc_GetEAAList_Result> Proc_GetEAAList(string username, string project_name, string location_name, string creater_name, string application_code)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var project_nameParameter = project_name != null ?
                new ObjectParameter("project_name", project_name) :
                new ObjectParameter("project_name", typeof(string));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            var creater_nameParameter = creater_name != null ?
                new ObjectParameter("creater_name", creater_name) :
                new ObjectParameter("creater_name", typeof(string));
    
            var application_codeParameter = application_code != null ?
                new ObjectParameter("application_code", application_code) :
                new ObjectParameter("application_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetEAAList_Result>("Proc_GetEAAList", usernameParameter, project_nameParameter, location_nameParameter, creater_nameParameter, application_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetEquipmentHandoverFormList_Result> Proc_GetEquipmentHandoverFormList(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetEquipmentHandoverFormList_Result>("Proc_GetEquipmentHandoverFormList", usernameParameter);
        }
    
        public virtual ObjectResult<Proc_GetEquipmentsList_Result> Proc_GetEquipmentsList(Nullable<bool> isadmin, string role, string username, string text, string text1, string text2, string categoryCode)
        {
            var isadminParameter = isadmin.HasValue ?
                new ObjectParameter("isadmin", isadmin) :
                new ObjectParameter("isadmin", typeof(bool));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            var text1Parameter = text1 != null ?
                new ObjectParameter("text1", text1) :
                new ObjectParameter("text1", typeof(string));
    
            var text2Parameter = text2 != null ?
                new ObjectParameter("text2", text2) :
                new ObjectParameter("text2", typeof(string));
    
            var categoryCodeParameter = categoryCode != null ?
                new ObjectParameter("categoryCode", categoryCode) :
                new ObjectParameter("categoryCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetEquipmentsList_Result>("Proc_GetEquipmentsList", isadminParameter, roleParameter, usernameParameter, textParameter, text1Parameter, text2Parameter, categoryCodeParameter);
        }
    
        public virtual ObjectResult<Proc_GetGetListComponentEquips_Result> Proc_GetGetListComponentEquips(string system_code)
        {
            var system_codeParameter = system_code != null ?
                new ObjectParameter("system_code", system_code) :
                new ObjectParameter("system_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetGetListComponentEquips_Result>("Proc_GetGetListComponentEquips", system_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetListEquipInLocation_Result> Proc_GetListEquipInLocation(Nullable<int> location_id)
        {
            var location_idParameter = location_id.HasValue ?
                new ObjectParameter("location_id", location_id) :
                new ObjectParameter("location_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetListEquipInLocation_Result>("Proc_GetListEquipInLocation", location_idParameter);
        }
    
        public virtual ObjectResult<Proc_GetListProjectInLocation_Result> Proc_GetListProjectInLocation(Nullable<int> location_id)
        {
            var location_idParameter = location_id.HasValue ?
                new ObjectParameter("location_id", location_id) :
                new ObjectParameter("location_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetListProjectInLocation_Result>("Proc_GetListProjectInLocation", location_idParameter);
        }
    
        public virtual ObjectResult<Proc_GetListStandardEquips_Result> Proc_GetListStandardEquips(string standard_code)
        {
            var standard_codeParameter = standard_code != null ?
                new ObjectParameter("standard_code", standard_code) :
                new ObjectParameter("standard_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetListStandardEquips_Result>("Proc_GetListStandardEquips", standard_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetLocationList_Result> Proc_GetLocationList(string username, Nullable<int> parent_id, string location_name)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var parent_idParameter = parent_id.HasValue ?
                new ObjectParameter("parent_id", parent_id) :
                new ObjectParameter("parent_id", typeof(int));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetLocationList_Result>("Proc_GetLocationList", usernameParameter, parent_idParameter, location_nameParameter);
        }
    
        public virtual ObjectResult<Proc_GetMemberListInLocation_Result> Proc_GetMemberListInLocation(Nullable<int> location_id)
        {
            var location_idParameter = location_id.HasValue ?
                new ObjectParameter("location_id", location_id) :
                new ObjectParameter("location_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetMemberListInLocation_Result>("Proc_GetMemberListInLocation", location_idParameter);
        }
    
        public virtual ObjectResult<Proc_GetMembersList_Result> Proc_GetMembersList(Nullable<bool> isadmin, string role, string username, string text)
        {
            var isadminParameter = isadmin.HasValue ?
                new ObjectParameter("isadmin", isadmin) :
                new ObjectParameter("isadmin", typeof(bool));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var textParameter = text != null ?
                new ObjectParameter("text", text) :
                new ObjectParameter("text", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetMembersList_Result>("Proc_GetMembersList", isadminParameter, roleParameter, usernameParameter, textParameter);
        }
    
        public virtual ObjectResult<Proc_GetNeedImportEAAList_Result> Proc_GetNeedImportEAAList(string username, string project_name, string location_name, string creater_name, string application_code)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var project_nameParameter = project_name != null ?
                new ObjectParameter("project_name", project_name) :
                new ObjectParameter("project_name", typeof(string));
    
            var location_nameParameter = location_name != null ?
                new ObjectParameter("location_name", location_name) :
                new ObjectParameter("location_name", typeof(string));
    
            var creater_nameParameter = creater_name != null ?
                new ObjectParameter("creater_name", creater_name) :
                new ObjectParameter("creater_name", typeof(string));
    
            var application_codeParameter = application_code != null ?
                new ObjectParameter("application_code", application_code) :
                new ObjectParameter("application_code", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetNeedImportEAAList_Result>("Proc_GetNeedImportEAAList", usernameParameter, project_nameParameter, location_nameParameter, creater_nameParameter, application_codeParameter);
        }
    
        public virtual ObjectResult<Proc_GetParentCategoriesList_Result> Proc_GetParentCategoriesList(Nullable<int> level)
        {
            var levelParameter = level.HasValue ?
                new ObjectParameter("level", level) :
                new ObjectParameter("level", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetParentCategoriesList_Result>("Proc_GetParentCategoriesList", levelParameter);
        }
    
        public virtual ObjectResult<Proc_GetProjectList_Result> Proc_GetProjectList(string username, string project_name)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            var project_nameParameter = project_name != null ?
                new ObjectParameter("project_name", project_name) :
                new ObjectParameter("project_name", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetProjectList_Result>("Proc_GetProjectList", usernameParameter, project_nameParameter);
        }
    
        public virtual ObjectResult<Proc_GetResultIRAList_Result> Proc_GetResultIRAList(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("username", username) :
                new ObjectParameter("username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetResultIRAList_Result>("Proc_GetResultIRAList", usernameParameter);
        }
    
        public virtual ObjectResult<Proc_GetRolesList_Result> Proc_GetRolesList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Proc_GetRolesList_Result>("Proc_GetRolesList");
        }
    
        public virtual int Proc_ImportEquipment(string system_equipment_code, string standard_equipment_code, string equipment_name, string serial_no, string unit, string specification, string type_equipment, string note, string category_code, Nullable<decimal> price)
        {
            var system_equipment_codeParameter = system_equipment_code != null ?
                new ObjectParameter("system_equipment_code", system_equipment_code) :
                new ObjectParameter("system_equipment_code", typeof(string));
    
            var standard_equipment_codeParameter = standard_equipment_code != null ?
                new ObjectParameter("standard_equipment_code", standard_equipment_code) :
                new ObjectParameter("standard_equipment_code", typeof(string));
    
            var equipment_nameParameter = equipment_name != null ?
                new ObjectParameter("equipment_name", equipment_name) :
                new ObjectParameter("equipment_name", typeof(string));
    
            var serial_noParameter = serial_no != null ?
                new ObjectParameter("serial_no", serial_no) :
                new ObjectParameter("serial_no", typeof(string));
    
            var unitParameter = unit != null ?
                new ObjectParameter("unit", unit) :
                new ObjectParameter("unit", typeof(string));
    
            var specificationParameter = specification != null ?
                new ObjectParameter("specification", specification) :
                new ObjectParameter("specification", typeof(string));
    
            var type_equipmentParameter = type_equipment != null ?
                new ObjectParameter("type_equipment", type_equipment) :
                new ObjectParameter("type_equipment", typeof(string));
    
            var noteParameter = note != null ?
                new ObjectParameter("note", note) :
                new ObjectParameter("note", typeof(string));
    
            var category_codeParameter = category_code != null ?
                new ObjectParameter("category_code", category_code) :
                new ObjectParameter("category_code", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Proc_ImportEquipment", system_equipment_codeParameter, standard_equipment_codeParameter, equipment_nameParameter, serial_noParameter, unitParameter, specificationParameter, type_equipmentParameter, noteParameter, category_codeParameter, priceParameter);
        }
    }
}