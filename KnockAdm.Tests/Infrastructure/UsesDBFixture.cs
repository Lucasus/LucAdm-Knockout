using System;
using System.Data.Entity.Migrations;

namespace LucAdm.Tests
{
    public sealed class UsesDbFixture : IDisposable
    {
        public UsesDbFixture()
        {
            new DbMigrator(new MigrationConfiguration()).Update();
            new PersistenceContext().ResetDbState();
            AutoMapperConfig.Register();
        }

        public void Dispose()
        {
        }
    }
}