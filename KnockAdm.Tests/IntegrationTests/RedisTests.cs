using FluentAssertions;
using StackExchange.Redis;
using Xunit;

namespace LucAdm.Tests
{
    public class RedisTests
    {
        [NamedFact(Skip = "Experiments")]
        [Trait("Category", "Integration-Experimental")]
        public void Should_Correctly_Connect_To_Redis()
        {
            var redis = ConnectionMultiplexer.Connect("192.168.1.193:6379");

            redis.IsConnected.Should().BeTrue();
        }
    }
}