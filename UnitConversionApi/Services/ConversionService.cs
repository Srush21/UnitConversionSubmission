
namespace UnitConversionApi.Services;

public class ConversionService
{
    private static readonly Dictionary<string, double> LengthUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Meter"] = 1,
            ["Kilometer"] = 1000,
            ["Centimeter"] = 0.01,
            ["Millimeter"] = 0.001,
            ["Mile"] = 1609.344,
            ["Foot"] = 0.3048,
            ["Inch"] = 0.0254,
            ["Yard"] = 0.9144
        };

    private static readonly Dictionary<string, double> WeightUnits =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Gram"] = 1,
            ["Kilogram"] = 1000,
            ["Milligram"] = 0.001,
            ["Pound"] = 453.59237,
            ["Ounce"] = 28.349523125
        };

    public double Convert(
        string category,
        string fromUnit,
        string toUnit,
        double value)
    {
        if (!double.IsFinite(value))
            throw new ArgumentException(
                "Value must be a finite number.");

        if (category.Equals(
            "Length", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertUsingFactors(
                LengthUnits, fromUnit, toUnit, value);
        }

        if (category.Equals(
            "Weight", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertUsingFactors(
                WeightUnits, fromUnit, toUnit, value);
        }

        if (category.Equals(
            "Temperature", StringComparison.OrdinalIgnoreCase))
        {
            return ConvertTemperature(fromUnit, toUnit, value);
        }

        throw new ArgumentException(
            $"Unsupported category: {category}");
    }

    private static double ConvertUsingFactors(
        Dictionary<string, double> units,
        string fromUnit,
        string toUnit,
        double value)
    {
        if (!units.TryGetValue(fromUnit, out double fromFactor))
            throw new ArgumentException(
                $"Unsupported source unit: {fromUnit}");

        if (!units.TryGetValue(toUnit, out double toFactor))
            throw new ArgumentException(
                $"Unsupported target unit: {toUnit}");

        return value * fromFactor / toFactor;
    }

    private static double ConvertTemperature(
        string fromUnit,
        string toUnit,
        double value)
    {
        double celsius = fromUnit.ToUpperInvariant() switch
        {
            "CELSIUS" or "C" => value,
            "FAHRENHEIT" or "F" => (value - 32) * 5 / 9,
            "KELVIN" or "K" => value - 273.15,
            _ => throw new ArgumentException(
                $"Unsupported source unit: {fromUnit}")
        };

        double result = toUnit.ToUpperInvariant() switch
        {
            "CELSIUS" or "C" => celsius,
            "FAHRENHEIT" or "F" => celsius * 9 / 5 + 32,
            "KELVIN" or "K" => celsius + 273.15,
            _ => throw new ArgumentException(
                $"Unsupported target unit: {toUnit}")
        };

        if (toUnit.Equals("K", StringComparison.OrdinalIgnoreCase) ||
            toUnit.Equals("Kelvin", StringComparison.OrdinalIgnoreCase))
        {
            if (result < 0)
                throw new ArgumentException(
                    "Temperature cannot be below absolute zero.");
        }

        return result;
    }
}