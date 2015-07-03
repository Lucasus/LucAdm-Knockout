using System.Threading.Tasks;
namespace KnockAdm
{
    public class EmailUnique : IRule
    {
        private readonly UserQueryService _userQueryService;
        private string _email;

        public EmailUnique(UserQueryService userQueryService, string email)
        {
            _userQueryService = userQueryService;
            _email = email;
        }

        public string ErrorMessage { get { return "Email must be unique"; } }

        public string Name { get { return PropertyName.Get((User x) => x.Email); } }

        public async Task<bool> CheckAsync()
        {
            return (await _userQueryService.ExistsByEmailAsync(_email)) == false;
        }
    }
}