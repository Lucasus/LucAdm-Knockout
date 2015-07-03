using FluentAssertions;
using LucAdm.DataGen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LucAdm.Tests
{
    public class UserQueryTests : IClassFixture<UsesDbFixture>
    {
        [NamedFact, Trait("Category", "Integration")]
        public async Task UserQuery_Get_Should_Return_Correct_Users()
        {
            var queryService = new UserQueryService(new PersistenceContext().ResetDbState());

            var users = await queryService.GetAsync(new GetUsersQuery()
            {
                Page = 1,
                PageSize = 5,
                SearchTerm = "g"
            });

            users.Total.Should().Be(2);
            users.List.Select(x => x.UserName).ToList().ShouldAllBeEquivalentTo(new List<string>() { Users.GandalfTheAdmin.UserName, Users.Legolas.UserName });
        }
    }
}