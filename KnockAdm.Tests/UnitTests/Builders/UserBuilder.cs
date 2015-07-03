namespace LucAdm.Tests
{
    public class UserBuilder : ObjectBuilder<User>
    {
        public UserBuilder Create()
        {
            _instance = new User
            {
                UserName = "userName",
                Email = "email@email.com",
                Active = true
            };
            return this;
        }

        public UserBuilder With(string userName = null, string email = null)
        {
            _instance.UserName = userName ?? _instance.UserName;
            _instance.Email = email ?? _instance.Email;
            return this;
        }
    }
}