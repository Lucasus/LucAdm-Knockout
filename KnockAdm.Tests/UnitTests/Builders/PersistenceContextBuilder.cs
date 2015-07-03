using NSubstitute;
using System.Collections.Generic;

namespace KnockAdm.Tests
{
    public class PersistenceContextBuilder : ObjectBuilder<PersistenceContext>
    {
        public PersistenceContextBuilder Create()
        {
            _instance = Substitute.For<PersistenceContext>();
            return this;
        }

        public PersistenceContextBuilder With(IList<User> users = null)
        {
            var usersMock = (users ?? new List<User>()).AsMock();
            _instance.Users.Returns(usersMock);
            _instance.Set<User>().Returns(usersMock);
            return this;
        }
    }
}