//
// Klasa przerobiona na EF z  http://lostechies.com/jimmybogard/2012/10/18/isolating-database-data-in-integration-tests/
//
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace LucAdm.DataGen
{
    public class DbDataDeleter
    {
        private readonly DbContext _context;
        private readonly string[] _ignoredTables = {"sysdiagrams", "__MigrationHistory"};

        public DbDataDeleter(DbContext context)
        {
            _context = context;
        }

        public virtual void DeleteAllData()
        {
            var allTables = GetAllTables();
            var allRelationships = GetRelationships();
            var tablesToDelete = BuildTableList(allTables, allRelationships);
            var deleteSql = BuildTableSql(tablesToDelete);
            _context.Database.ExecuteSqlCommand(deleteSql);
        }

        private IList<string> GetAllTables()
        {
            return _context.Database.SqlQuery<string>("select name from sys.tables")
                .Except(_ignoredTables)
                .ToList();
        }

        private IList<Relationship> GetRelationships()
        {
            var relationshipsSql =
                @"select
                    so_pk.name as PrimaryKeyTable
                   ,so_fk.name as ForeignKeyTable
                from sysforeignkeys sfk
                    inner join sysobjects so_pk on sfk.rkeyid = so_pk.id
                    inner join sysobjects so_fk on sfk.fkeyid = so_fk.id
                order by so_pk.name, so_fk.name";

            var objectContext = ((IObjectContextAdapter) _context).ObjectContext;
            return objectContext.ExecuteStoreQuery<Relationship>(relationshipsSql).ToList();
        }

        private string BuildTableSql(IEnumerable<string> tablesToDelete)
        {
            var sb = new StringBuilder();
            foreach (var tableName in tablesToDelete)
            {
                sb.Append(string.Format("delete from [{0}];", tableName));
            }
            return sb.ToString();
        }

        private string[] BuildTableList(ICollection<string> allTables, ICollection<Relationship> allRelationships)
        {
            var tablesToDelete = new List<string>();

            while (allTables.Any())
            {
                var leafTables = allTables.Except(allRelationships.Select(rel => rel.PrimaryKeyTable)).ToArray();

                tablesToDelete.AddRange(leafTables);

                foreach (var leafTable in leafTables)
                {
                    allTables.Remove(leafTable);
                    var relToRemove = allRelationships.Where(rel => rel.ForeignKeyTable == leafTable).ToArray();
                    foreach (var rel in relToRemove)
                    {
                        allRelationships.Remove(rel);
                    }
                }
            }

            return tablesToDelete.ToArray();
        }

        private class Relationship
        {
            public string PrimaryKeyTable { get; set; }
            public string ForeignKeyTable { get; set;}
        }
    }
}