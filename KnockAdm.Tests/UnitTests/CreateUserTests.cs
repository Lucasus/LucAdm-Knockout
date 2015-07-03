using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LucAdm.Tests
{
    public class CreateUserTests : IClassFixture<ServiceFixture>
    {
        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_With_Correct_Command_Should_Create_User()
        {
            var context = Some.MockedContext().With(new List<User>()).Build();
            var userRepository = Substitute.For<Repository<User>>(context);

            var command = Some.CreateUserCommand().With(password: "password", rePassword: "password").Build();
            var response = await Some.UserService(context, userRepository).CreateUserAsync(command);

            response.ValidationResult.Errors.Should().BeEmpty();
            userRepository.Received(1).Add(Arg.Is<User>(x => !string.IsNullOrEmpty(x.HashedPassword) && x.HashedPassword != command.Password));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_Without_UserName_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(userName: ""));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.UserName));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_Invalid_Email_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(email: "invalid"));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.Email));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_Invalid_UserName_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(userName: "!%^^#$#$"));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.UserName));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_Without_Password_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(password: ""));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.Password));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_With_Too_Short_Password_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(password: "sh", rePassword: "sh"));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.Password));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_With_Incorrectly_Repeated_Password_Should_Return_Validation_Error()
        {
            var response = await Some.UserService().CreateUserAsync(Some.CreateUserCommand().With(password: "pass", rePassword: "rePass"));
            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.RepeatedPassword));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_With_Duplicated_UserName_Should_Return_Validation_Error()
        {
            var response = await Some.UserService(Some.MockedContext().With(Some.User().With(userName: "existingName").ToList()))
                .CreateUserAsync(Some.CreateUserCommand().With(userName: "existingName"));

            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.UserName));
        }

        [NamedFact, Trait("Category", "Unit")]
        public async Task CreateUser_With_Duplicated_Email_Should_Return_Validation_Error()
        {
            var response = await Some.UserService(Some.MockedContext().With(Some.User().With(userName: "u1", email: "existing@email.com").ToList()))
                .CreateUserAsync(Some.CreateUserCommand().With(userName: "u2", email: "existing@email.com"));

            response.ValidationResult.Errors.Should().ContainKey(PropertyName.Get((CreateUserCommand x) => x.Email));
        }
    }
}