using System.Data.Entity;

namespace KnockAdm.DataGen
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