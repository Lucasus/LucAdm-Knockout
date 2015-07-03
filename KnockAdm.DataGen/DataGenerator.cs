using System.Data.Entity;

namespace LucAdm.DataGen
{
    public class DataGenerator : GeneratorBase
    {
        public DataGenerator(DbContext context, EnvironmentEnum environment) : base(context, environment)
        {
        }

        public void Generate()
        {
            Save(new Users());
        }
    }
}