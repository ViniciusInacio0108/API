using Buberbreakfast.Contracts.Breakfast;
using Microsoft.AspNetCore.Mvc;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfast;

namespace BuberBreakfast.Controllers;

[ApiController]
[Route("[controller]")]

public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        // Mapping the data from the request to our language to work with
        var breakfast = new Breakfast(
            Guid.NewGuid(),
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // Here is our logic through a interface where we comunicate to our service
        _breakfastService.CreateBreakfast(breakfast);

        // Taking the data from our application and converting it back to the response defintion
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        // Actually responding it
        return CreatedAtAction(
            nameof(GetABreakfast),
            new { id = breakfast.Id },
            response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetABreakfast(Guid id)
    {
        // Let's retrive the breakfast based on the id
        Breakfast breakfast = _breakfastService.GetBreakfast(id);

        //Now let's map this retrived breakfast to the response
        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateBreaksfast(UpsertBreakfastRequest request, Guid id)
    {
        // Turn the request into a language dealing data
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        // Update the breakfast
        var hasBeenUpsert = _breakfastService.UpsertBreakfast(breakfast, id);

        // If updated then return Created if not then No Content
        if (hasBeenUpsert)
        {
            // Transform date to response type
            var response = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                DateTime.UtcNow,
                breakfast.Savory,
                breakfast.Sweet
            );

            return CreatedAtAction(
                nameof(GetABreakfast),
                new { id = breakfast.Id },
                response
            );
        }
        else
        {
            return NoContent();

        }

    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {
        // Call the remove method
        _breakfastService.DeleteBreakfast(id);

        return NoContent();
    }

    [HttpGet]
    public IActionResult GetAllBreakfasts()
    {
        // Get all the methods
        var listOfBreakfasts = _breakfastService.AllBreakfasts();

        // Transform into response data type
        var response = new List<BreakfastResponse> { };
        foreach (Breakfast breakfast in listOfBreakfasts)
        {
            var breakfastResponse = new BreakfastResponse(
                breakfast.Id,
                breakfast.Name,
                breakfast.Description,
                breakfast.StartDateTime,
                breakfast.EndDateTime,
                breakfast.LastModifiedDateTime,
                breakfast.Savory,
                breakfast.Sweet
            );
            response.Add(breakfastResponse);
        }

        return Ok(response);
    }
}