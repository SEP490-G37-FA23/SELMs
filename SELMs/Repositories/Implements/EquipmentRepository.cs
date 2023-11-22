using Dapper;
using SELMs.Api.DTOs;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteEquipment(int id)
        {
            dynamic equipment = db.Equipments.Where(e => e.equipment_id == id).FirstOrDefault();
            if (equipment != null) db.Equipments.Remove(equipment);
            db.SaveChangesAsync();

        }

        public dynamic GetEquipment(int id)
        {
            dynamic equipment = db.Equipments.Where(e => e.equipment_id == id).FirstOrDefault();
            return equipment;
        }

        public void SaveEquipment(Equipment equipment,int location_id, List<EquipComponentDTO> ListComponentEquips)
        {
            db.Equipments.Add(equipment);
            Equipment_Location_History his = new Equipment_Location_History();
            his.location_id = location_id;
            his.system_equipment_code = equipment.system_equipment_code;
            his.from_date = DateTime.Now;
            db.Equipment_Location_History.Add(his);
            foreach(EquipComponentDTO item in ListComponentEquips)
            {
                Equipment_Component component = new Equipment_Component();
                component.system_equipment_code_parent = equipment.system_equipment_code;
                component.system_equipment_code_component = item.system_equipment_code;
                db.Equipment_Component.Add(component);
            }
           
            db.SaveChangesAsync();
        }

        public void SaveEquipments(List<Equipment> equipments)
        {
            db.Equipments.AddRange(equipments);
            db.SaveChangesAsync();
        }

        public dynamic GetEquipmentList(Argument arg)
        {
            dynamic equipments = null;
            equipments = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentsList", new
            {
                username = arg.username,
                role=arg.role,
                isadmin = arg.isadmin,
                text1 = arg.text1,
                text2 = arg.text2,
                text = arg.text,
                categoryCode = arg.categoryCode,

            }
                , commandType: CommandType.StoredProcedure);
            return equipments;
        }

        public void UpdateEquipment(Equipment equipment)
        {
            Equipment orgEquipment = db.Equipments.Where(e => e.equipment_id == equipment.equipment_id).FirstOrDefault();
            db.Entry(orgEquipment).CurrentValues.SetValues(equipment);
            db.SaveChangesAsync();
        }

        public Equipment GetLastEquipment()
        {
            Equipment result = db.Equipments.OrderByDescending(e => e.equipment_id).FirstOrDefault();
            return result;
        }

        public dynamic GetDetailEquipment(string code)
        {
            dynamic equipments = null;
            equipments = db.Database.Connection.QueryAsync<dynamic>("Proc_GetDetailEquipment", new
            {
                system_code = code

            }
                , commandType: CommandType.StoredProcedure);
            return equipments;
        }

        public List<EquipComponentDTO> GetListComponentEquips(string code)
        {
            List<EquipComponentDTO> equipments = new List<EquipComponentDTO>();
            equipments = db.Database.Connection.Query<EquipComponentDTO>("Proc_GetGetListComponentEquips", new
            {
                system_code = code

            }
                , commandType: CommandType.StoredProcedure).ToList();
            return equipments;
        }

        public dynamic GetEquipmentList()
        {
            dynamic result = db.Equipments.ToListAsync();
            return result;
        }

        public dynamic GetStandardEquipmentList(Argument args)
        {
            dynamic equipments =  db.Database.Connection.Query<dynamic>("Proc_GetListStandardEquips", new
            {
                standard_code = args.text

            }
                , commandType: CommandType.StoredProcedure).ToList();
            return equipments;
        }
    }
}