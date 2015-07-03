using System;

namespace KnockAdm.Tests
{
    public sealed class ServiceFixture : IDisposable
    {
        public ServiceFixture()
        {
            AutoMapperConfig.Register();
        }

        public void Dispose()
        {
        }
    }
}