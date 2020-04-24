using BackupCafeApp.DomainEntityModel;
using BackupCafeApp.DomainEntityModel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeApp.Persistence.Repositories
{
    public class TableRepository : ITableRepository
    {
        public AppDbContext db = new AppDbContext();
        public void AddTable(Table table)
        {
            db.Tables.Add(table);
            Save();
        }

        public void DeleteTable(Table table)
        {
            db.Tables.Remove(table);
            Save();
        }

        public Table GetTable(int? id)
        {
            Table table = db.Tables.Find(id);
            return table;
        }

        public IEnumerable<Table> GetTables()
        {
            return db.Tables.ToList();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void UpdateTable(Table table)
        {
            db.Entry(table).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
