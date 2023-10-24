using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SELMs.Repositories.Implements
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private SELMsContext db = new SELMsContext();
        public void DeleteEquipment(int id)
        {
            dynamic equipment = db.Equipments.Where(e => e.equipment_id == id).FirstOrDefault();
            if (equipment != null) db.Categories.Remove(equipment);
            db.SaveChangesAsync();
        }

        public dynamic GetEquipment(int id)
        {
            dynamic equipment = db.Equipments.Where(e => e.equipment_id == id).FirstOrDefault();
            return equipment;
        }

        public dynamic GetEquipmentList()
        {
            dynamic equipments = db.Equipments.ToListAsync();
            return equipments;
        }

        public void SaveEquipment(Equipment equipment)
        {
            db.Equipments.Add(equipment);
            db.SaveChangesAsync();
        }

        public void SaveEquipments(List<Equipment> equipments)
        {
            db.Equipments.AddRange(equipments);
            db.SaveChangesAsync();
        }

        public dynamic SearchEquipments(Argument arg)
        {
            dynamic equipments = null;
            equipments = db.Database.Connection.QueryAsync<dynamic>("Proc_GetEquipmentList", new
            {
                username = arg.username,
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
    }
}