using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Http;

namespace KnockAdm.Web
{
    public class ComponentsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility<EntityFrameworkFacility>();

            container.Register(Classes.FromAssemblyContaining(typeof(Repository<,>))
                .BasedOn(typeof(Repository<,>))
                .LifestyleTransient());

            container.Register(Component.For<UnitOfWork>()
                .LifestyleTransient());

            container.Register(Component.For<UnitOfWorkFactory>()
                .LifestyleTransient());

            container.Register(Classes.FromThisAssembly()
                .BasedOn<ApiController>()
                .LifestyleTransient());

            container.Register(Classes.FromAssemblyContaining<UserService>()
                .Where(x => x.Name.Contains("Service"))
                .LifestyleTransient());
        }
    }
}