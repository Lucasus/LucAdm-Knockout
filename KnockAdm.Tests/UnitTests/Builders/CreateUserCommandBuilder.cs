namespace LucAdm.Tests
{
    public class CreateUserCommandBuilder : ObjectBuilder<CreateUserCommand>
    {
        public CreateUserCommandBuilder Create()
        {
            _instance = new CreateUserCommand
            {
                Email = "email@email.com",
                Password = "somePassword",
                RepeatedPassword = "somePassword",
                UserName = "userName"
            };
            return this;
        }

        public CreateUserCommandBuilder With(string userName = null, string password = null, string rePassword = null, string email = null)
        {
            _instance.UserName = userName ?? _instance.UserName;
            _instance.Password = password ?? _instance.Password;
            _instance.RepeatedPassword = rePassword ?? _instance.RepeatedPassword;
            _instance.Email = email ?? _instance.Email;
            return this;
        }
    }
}