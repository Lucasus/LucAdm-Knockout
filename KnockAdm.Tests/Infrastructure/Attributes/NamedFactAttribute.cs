using System.Runtime.CompilerServices;
using Xunit;

namespace LucAdm.Tests
{
    public sealed class NamedFactAttribute : FactAttribute
    {
        public NamedFactAttribute([CallerMemberName] string methodName = null)
        {
            DisplayName = methodName.Replace('_', ' ').AddSpacesToSentence(preserveAcronyms: true, delimiter: ' ');
        }
    }
}