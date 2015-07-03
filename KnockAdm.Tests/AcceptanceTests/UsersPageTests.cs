using FluentAssertions;
using LucAdm.DataGen;
using Xunit;

namespace LucAdm.Tests
{
    public class UsersPageTests : IClassFixture<SeleniumFixture>
    {
        private readonly Browser _browser;
        private static bool firstLoad = true;

        public UsersPageTests(SeleniumFixture seleniumFixture)
        {
            _browser = seleniumFixture.Browser;
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_ShouldDisplay_Header()
        {
            var usersPage = preparePage();

            usersPage.Header.Should().Contain("Luc");
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_ShouldDisplay_ListOfUsers()
        {
            var usersPage = preparePage();

            usersPage.GetUsersList().Should().NotBeEmpty();
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_Can_Search_By_UserName()
        {
            var usersPage = preparePage();

            usersPage.SearchFor(Users.Frodo.UserName);
            var users = usersPage.GetUsersList(expectedCount: 1);

            users.Should().HaveCount(1);
            users.Should().Contain(item => item.Contains(Users.Frodo.UserName) && item.Contains(Users.Frodo.Email));
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_Can_Remove_User()
        {
            var usersPage = preparePage();

            usersPage.SearchFor(Users.Frodo.UserName);
            usersPage.GetUsersList(expectedCount: 1);
            usersPage.ClickRemoveFor(Users.Frodo.UserName);
            usersPage.AcceptRemove();

            usersPage.GetUsersList(expectedCount: 0).Should().BeEmpty();
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_Can_Add_User()
        {
            var newUserName = "BilboBaggins";
            var newEmail = "bilbo@bilbo.com";
            var usersPage = preparePage();

            usersPage.ClickAddUser();
            usersPage.FillUserForm(false, newUserName, newEmail, "somePassword");
            usersPage.ModalClickOK();
            usersPage.SearchFor(newUserName);
            var users = usersPage.GetUsersList(expectedCount: 1);

            users.Should().HaveCount(1);
            users.Should().Contain(item => item.Contains(newUserName) && item.Contains(newEmail));
        }

        [NamedFact, Trait("Category", "Acceptance")]
        public void UsersPage_Can_Edit_User()
        {
            var userName = Users.Frodo.UserName;
            var newEmail = "bilbo@bilbo.com";
            var usersPage = preparePage();

            usersPage.SearchFor(userName);
            usersPage.GetUsersList(expectedCount: 1);
            usersPage.ClickEditFor(userName);
            usersPage.FillUserForm(edit: true, email: newEmail);
            usersPage.ModalClickOK();
            usersPage.SearchFor(userName);

            var users = usersPage.GetUsersList(expectedCount: 1);

            users.Should().HaveCount(1);
            users.Should().Contain(item => item.Contains(userName) && item.Contains(newEmail));
        }

        private UsersPage preparePage()
        {
            new PersistenceContext().ResetDbState(EnvironmentEnum.Test);
            var usersPage = _browser.Load(new UsersPage());
            var timeout = firstLoad ? 15 : 5;
            firstLoad = false;
            usersPage.GetUsersList(timeout: timeout);
            return usersPage;
        }
    }
}