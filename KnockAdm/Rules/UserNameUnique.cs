using System.Threading.Tasks;
namespace KnockAdm
{
    public class UserNameUnique : IRule
    {
        private readonly UserQueryService _userQueryService;
        private string _userName;

        public UserNameUnique(UserQueryService userQueryService, string userName)
        {
            _userQueryService = userQueryService;
            _userName = userName;
        }

        public string ErrorMessage { get { return "User name must be unique"; } }

        public string Name { get { return PropertyName.Get((User x) => x.UserName); } }

        public async Task<bool> CheckAsync()
        {
            return (await _userQueryService.ExistsByUserNameAsync(_userName)) == false;
        }
    }
}