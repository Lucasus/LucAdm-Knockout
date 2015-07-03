using System.Collections.Generic;
using System.Linq;

namespace LucAdm.DataGen
{
    public class Users : Data<User>
    {
        [Environment(EnvironmentEnum.Dev, EnvironmentEnum.Test)] 
        public static User GandalfTheAdmin = new User
        {
            UserName = "gandalf",
            HashedPassword = new HashProvider().GetPasswordHash("adminPwd"),
            Email = "admin@admin.com",
            Active = true
        };

        [Environment(EnvironmentEnum.Dev, EnvironmentEnum.Test)] 
        public static User Legolas = new User
        {
            UserName = "Legolas",
            HashedPassword = new HashProvider().GetPasswordHash("legolasPwd"),
            Email = "legolas@legolas.com",
            Active = true
        };

        [Environment(EnvironmentEnum.Dev, EnvironmentEnum.Test)] 
        public static User Frodo = new User
        {
            UserName = "Frodo",
            HashedPassword = new HashProvider().GetPasswordHash("frodo12"),
            Email = "frodo@lucadm.com",
            Active = true
        };

        public override IEnumerable<User> GetGeneratedData(EnvironmentEnum environment)
        {
            if (environment == EnvironmentEnum.Dev || environment == EnvironmentEnum.Test)
            {
                return Enumerable.Range(1, environment == EnvironmentEnum.Dev ? 100 : 10).Select(i => new User
                {
                    UserName = "User" + i,
                    Email = "email@email" + i,
                    HashedPassword = new HashProvider().GetPasswordHash("password" + i),
                    Active = i%2 == 0
                });
            }
            return null;
        }
    }
}