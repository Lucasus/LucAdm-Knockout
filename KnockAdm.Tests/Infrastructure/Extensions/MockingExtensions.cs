using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LucAdm.Tests
{
    public static class MockingExtensions
    {
        public static DbSet<T> AsMock<T>(this IList<T> data)
             where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new FakeDbSet<T>(data);
            return mockSet;
        }
    }
}