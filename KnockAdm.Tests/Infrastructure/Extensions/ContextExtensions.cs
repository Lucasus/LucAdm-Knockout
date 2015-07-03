using LucAdm.DataGen;

namespace LucAdm.Tests
{
    public static class ContextExtensions
    {
        public static PersistenceContext ResetDbState(this PersistenceContext context,
            EnvironmentEnum envoronment = EnvironmentEnum.Test)
        {
            new DbDataDeleter(context).DeleteAllData();
            new DataGenerator(context, envoronment).Generate();
            return context;
        }
    }
}