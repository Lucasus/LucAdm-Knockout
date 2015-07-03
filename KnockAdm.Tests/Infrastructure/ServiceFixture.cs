using System;

namespace LucAdm.Tests
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