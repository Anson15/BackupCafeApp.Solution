using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupCafeApp.DomainEntityModel.Interface
{
    public interface ITableRepository
    {
        IEnumerable<Table> GetTables();
        Table GetTable(int? id);
        void AddTable(Table table);
        void DeleteTable(Table table);
        void UpdateTable(Table table);
        void Save();
    }
}
