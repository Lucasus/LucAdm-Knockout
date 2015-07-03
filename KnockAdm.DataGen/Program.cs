using System;
using System.Data.Entity.Migrations;

namespace LucAdm.DataGen
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var context = new PersistenceContext();
            new DbDataDeleter(context).DeleteAllData();
            new DbMigrator(new MigrationConfiguration()).Update();
            new DataGenerator(context, EnvironmentEnum.Dev).Generate();
            Console.Out.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}