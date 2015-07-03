using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace LucAdm.DataGen
{
    public abstract class GeneratorBase
    {
        private readonly DbContext _context;
        private readonly EnvironmentEnum _environment;

        protected GeneratorBase(DbContext context, EnvironmentEnum environment)
        {
            _context = context;
            _environment = environment;
        }


        protected void Save<T>(Data<T> data)
            where T : Entity, new()
        {
            var entities = data.GetType()
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field =>
                {
                    if (field.FieldType != typeof (T))
                    {
                        return false;
                    }

                    var envAttribute = field.GetCustomAttributes<EnvironmentAttribute>().FirstOrDefault();
                    if (envAttribute != null)
                    {
                        return envAttribute.Environments.Any(env => env == _environment || env == EnvironmentEnum.All);
                    }
                    return true;
                })
                .Select(prop => (T) prop.GetValue(null)).ToList();

            var additionalEntites = data.GetGeneratedData(_environment);
            if (additionalEntites != null)
            {
                entities = entities.Union(additionalEntites).ToList();
            }

            foreach (var ent in entities)
            {
                _context.Set<T>().Add(ent);
            }
            Console.Write("Creating " + typeof (T).Name + "... ");
            _context.SaveChanges();

            var addedEntitiesCount = _context.Set<T>().Count();
            if(addedEntitiesCount != entities.Count)
            {
                throw new Exception(String.Format("{0}: not all entries added (expected count: {1}, actual count: {2}",
                    typeof(T).Name, entities.Count, addedEntitiesCount));
            }
            Console.WriteLine(" DONE");
        }
    }
}