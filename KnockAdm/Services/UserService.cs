using System;
using System.Threading.Tasks;

namespace KnockAdm
{
    public class UserService
    {
        private readonly UnitOfWorkFactory _unitOfWorkFactory;
        private readonly UserQueryService _userQueryService;
        private readonly Repository<User> _userRepository;

        public UserService(Repository<User> userRepository, UserQueryService userQueryService, UnitOfWorkFactory unitOfWorkFactory)
        {
            _userQueryService = userQueryService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _userRepository = userRepository;
        }

        public Task<OperationResponse<int>> CreateUserAsync(CreateUserCommand command)
        {
            return  command.Validate(new CreateUserCommandValidator())
                .CheckAsync(new UserNameUnique(_userQueryService, command.UserName))
                .CheckAsync(new EmailUnique(_userQueryService, command.Email))
                .IfValidAsync(function: async () => 
            {
                var user = new User
                {
                    Active = false,
                    Email = command.Email,
                    HashedPassword = new HashProvider().GetPasswordHash(command.Password),
                    UserName = command.UserName
                };

                await _unitOfWorkFactory.DoAsync(work =>
                {
                    _userRepository.Add(user);
                });

                return user.Id.AsResponse();
            });
        }

        public Task<OperationResponse> UpdateUserAsync(UpdateUserCommand command)
        {
            return command.Validate(new UpdateUserCommandValidator())
                .IfValidAsync( () => _unitOfWorkFactory.DoAsync(() =>
            {
                var user = _userRepository.GetById(command.Id);
                user.Email = command.Email;
                user.UserName = command.UserName;
                _userRepository.Update(user);
            }));
        }

        public Task<OperationResponse> DeleteUserAsync(Validated<int> id)
        {
            return id.Validate(new IdValidator())
                .IfValidAsync(() => _unitOfWorkFactory.DoAsync(() =>
            {
                _userRepository.Delete(id);
            }));
        }
    }
}