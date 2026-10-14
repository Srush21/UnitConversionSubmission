Unit Conversion API



A REST API built with ASP.NET Core and C# to convert values between units of length, weight, and temperature.



Technologies

C#

ASP.NET Core Web API

.NET 9

xUnit

Supported units

Length: Meter, Kilometer, Centimeter, Millimeter, Mile, Foot, Inch, Yard

Weight: Gram, Kilogram, Milligram, Pound, Ounce

Temperature: Celsius, Fahrenheit, Kelvin

Run the API



Install the .NET 9 SDK, then run:



cd UnitConversionApi

dotnet restore

dotnet build

dotnet run



API endpoint



POST /api/conversions



Example request:



{

&#x20; "category": "Length",

&#x20; "fromUnit": "Kilometer",

&#x20; "toUnit": "Meter",

&#x20; "value": 5

}





Example response:



{

&#x20; "category": "Length",

&#x20; "fromUnit": "Kilometer",

&#x20; "toUnit": "Meter",

&#x20; "inputValue": 5,

&#x20; "result": 5000

}



Run automated tests



From the repository root:



dotnet test .\\UnitConversionApi.Tests\\UnitConversionApi.Tests.csproj



Project structure

UnitConversionApi/Controllers — API endpoints

UnitConversionApi/Services — conversion logic

UnitConversionApi.Tests — automated tests

