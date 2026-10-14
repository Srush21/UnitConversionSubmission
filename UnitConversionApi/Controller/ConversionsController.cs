using Microsoft.AspNetCore.Mvc;
using UnitConversionApi.Services;

namespace UnitConversionApi.Controllers;

public record ConversionRequest(
    string Category,
    string FromUnit,
    string ToUnit,
    double Value);

public record ConversionResponse(
    string Category,
    string FromUnit,
    string ToUnit,
    double InputValue,
    double Result);

[ApiController]
[Route("api/[controller]")]
public class ConversionsController : ControllerBase
{
    private readonly ConversionService _service;

    public ConversionsController(ConversionService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<ConversionResponse> Convert(
        [FromBody] ConversionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Category) ||
            string.IsNullOrWhiteSpace(request.FromUnit) ||
            string.IsNullOrWhiteSpace(request.ToUnit))
        {
            return BadRequest(new
            {
                error = "Category and units are required."
            });
        }

        try
        {
            double result = _service.Convert(
                request.Category,
                request.FromUnit,
                request.ToUnit,
                request.Value);

            return Ok(new ConversionResponse(
                request.Category,
                request.FromUnit,
                request.ToUnit,
                request.Value,
                result));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}