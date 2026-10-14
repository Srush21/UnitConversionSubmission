using UnitConversionApi.Services;
using Xunit;

namespace UnitConversionApi.Tests;

public class UnitTest1
{
    private readonly ConversionService _service = new ();

    [Theory]
    [InlineData("Length", "Kilometer", "Meter", 5, 5000)]
    [InlineData("Weight", "Kilogram", "Gram", 2, 2000)]
    [InlineData("Temperature", "Celsius", "Fahrenheit", 100, 212)]
    public void Convert_ReturnsExpectedResult(
        string category,
        string fromUnit,
        string toUnit,
        double value,
        double expected)
    {
        var result = _service.Convert(
            category, fromUnit, toUnit, value);

        Assert.Equal(expected, result, 5);
    }

    [Fact]
    public void Convert_InvalidUnit_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() =>
            _service.Convert(
                "Length", "InvalidUnit", "Meter", 5));
    }
}