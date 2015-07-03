using System.Runtime.CompilerServices;
using Xunit;

namespace KnockAdm.Tests
{
    public sealed class NamedFactAttribute : FactAttribute
    {
        public NamedFactAttribute([CallerMemberName] string methodName = null)
        {
            DisplayName = methodName.Replace('_', ' ').AddSpacesToSentence(preserveAcronyms: true, delimiter: ' ');
        }
    }
}