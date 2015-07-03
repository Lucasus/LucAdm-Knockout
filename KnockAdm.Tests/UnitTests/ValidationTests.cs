using AutoMapper;
using FluentAssertions;
using Xunit;

namespace LucAdm.Tests
{
    public class ValidationTests
    {
        [NamedTheory, Trait("Category", "Unit")]
        [InlineData("5", true, 5)]
        [InlineData("notANumber", false, default(int))]
        [InlineData(null, false, default(int))]
        public void Mapping_IntAsString_Maps_To_CorrectValidatedInt(string initialValue, bool expectedValid, int expectedValue)
        {
            Mapper.CreateMap<ExampleViewModel, ExampleRequest>();
            var source = new ExampleViewModel { IntProperty = initialValue };

            var destination = Mapper.Map(source, new ExampleRequest());

            destination.IntProperty.IsValid.Should().Be(expectedValid);
            destination.IntProperty.Value.Should().Be(expectedValue);
        }

        [NamedFact, Trait("Category", "Unit")]
        public void Mapping_Null_ToValidatedNullableInt_Maps_To_ValidValidatedInt_WithNullValue()
        {
            Mapper.CreateMap<ExampleViewModel, ExampleRequest>();
            var source = new ExampleViewModel { NullableIntProperty = null };

            var destination = Mapper.Map(source, new ExampleRequest());

            destination.NullableIntProperty.IsValid.Should().BeTrue();
            destination.NullableIntProperty.Value.Should().Be(null);
        }

        private class ExampleViewModel
        {
            public string IntProperty { get; set; }

            public string NullableIntProperty { get; set; }
        }

        private class ExampleRequest
        {
            public Validated<int> IntProperty { get; set; }

            public Validated<int?> NullableIntProperty { get; set; }
        }
    }
}