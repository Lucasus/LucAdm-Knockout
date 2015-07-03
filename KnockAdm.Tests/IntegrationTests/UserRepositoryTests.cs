using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace LucAdm.Tests
{
    public class UserRepositoryTests : IClassFixture<UsesDbFixture>
    {
        // TODO: read https://github.com/scott-xu/EntityFramework.Testing
        [NamedFact, Trait("Category", "Integration")]
        public async Task User_Should_Be_Saved_Correctly()
        {
            var context = new PersistenceContext().ResetDbState();
            var userRepository = new Repository<User>(context);
            User newUser = Some.User();

            await new UnitOfWork(context).DoAsync(work => { userRepository.Add(newUser); });

            userRepository.GetById(newUser.Id).ShouldBeEquivalentTo(newUser);
        }
    }
}