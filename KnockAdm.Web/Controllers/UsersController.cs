using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace KnockAdm.Web
{
    public class UsersController : ApiController
    {
        private readonly UserQueryService _userQueryService;
        private readonly UserService _userService;

        public UsersController(UserQueryService userQueryService, UserService userService)
        {
            _userQueryService = userQueryService;
            _userService = userService;
        }

        public Task<UsersDto> GetAsync([FromUri] GetUsersQuery query)
        {
            return _userQueryService.GetAsync(query);
        }

        public Task<UserDto> GetAsync(int id)
        {
            return _userQueryService.GetByIdAsync(id);
        }

        public Task<OperationResponse<int>> PostAsync(CreateUserCommand command)
        {
            return _userService.CreateUserAsync(command);
        }

        public async Task<HttpResponseMessage> PutAsync(UpdateUserCommand command)
        {
            await _userService.UpdateUserAsync(command);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> DeleteAsync(Validated<int> id)
        {
            await _userService.DeleteUserAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
