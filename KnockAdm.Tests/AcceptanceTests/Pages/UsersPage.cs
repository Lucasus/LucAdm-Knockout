using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LucAdm.Tests
{
    public class UsersPage : PageObject
    {
        public override string Url { get { return "/"; } }

        public string Header { get { return Driver.ListByCss("header").FirstOrDefault().Text; } }

        public IList<string> GetUsersList(int? expectedCount = null, double timeout = 5)
        {
            return getUserElements(expectedCount, timeout).Select(x => x.Content()).ToList();
        }

        public void ClickRemoveFor(string userName)
        {
            var userToDelete = getUserElements().First(x => x.Text.Contains(userName));
            userToDelete.ElementFor(".cmdDelete").Click();
        }

        public void ClickEditFor(string userName)
        {
            var userToEdit = getUserElements().First(x => x.Text.Contains(userName));
            userToEdit.ElementFor(".cmdEdit").Click();
        }

        public void SearchFor(string searchTerm)
        {
            Driver.OverrideValueFor("input[ng-model=\"users.searchTerm\"", searchTerm);
            Driver.ElementFor("button[ng-click=\"search()\"").Click();
            Thread.Sleep(500);
        }

        public void AcceptRemove()
        {
            Driver.SwitchTo().Alert().Accept();
        }

        public void ClickAddUser()
        {
            Driver.ElementFor("button[ng-click=\"openModal('')\"").Click();
        }

        public void FillUserForm(bool edit, string userName = null, string email = null, string password = null)
        {
            Driver.WaitFor(userNameInputSelector());
            if (userName != null)
            {
                Driver.OverrideValueFor(userNameInputSelector(), userName);
            }
            if (email != null)
            {
                Driver.OverrideValueFor("input[ng-model=\"user.email\"", email, edit);
            }
            if (password != null)
            {
                Driver.OverrideValueFor("input[ng-model=\"user.password\"", password);
                Driver.OverrideValueFor("input[ng-model=\"user.repeatedPassword\"", password);
            }
        }

        public void ModalClickOK()
        {
            Driver.ElementFor("button[ng-click=\"save(user)\"").Click();
            Driver.WaitUntilHidden(userNameInputSelector());
        }

        private string userNameInputSelector()
        {
            return "input[ng-model=\"user.userName\"";
        }

        private IList<IWebElement> getUserElements(int? expectedCount = null, double timeout = 5)
        {
            return Driver.WaitForList(".user-item", expectedCount, timeout).ToList();
        }
    }
}