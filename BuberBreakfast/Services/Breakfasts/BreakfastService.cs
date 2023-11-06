using BuberBreakfast.Services.Breakfast;
using BuberBreakfast.Models;
using System.Collections.Immutable;
using Microsoft.VisualBasic;

namespace BuberBreakfast.Services;

public class BreakfastService : IBreakfastService
{

    private static readonly Dictionary<Guid, Models.Breakfast> _breakfasts = new();

    public List<Models.Breakfast> AllBreakfasts()
    {
        List<Models.Breakfast> breakfasts = _breakfasts.Values.ToList();

        return breakfasts;
    }

    public void CreateBreakfast(Models.Breakfast breakfast)
    {
        _breakfasts.Add(breakfast.Id, breakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public Models.Breakfast GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }

    public Boolean UpsertBreakfast(Models.Breakfast breakfast, Guid id)
    {
        if (_breakfasts.ContainsKey(id))
        {
            _breakfasts[id] = breakfast;
            return true;
        }
        else
        {
            _breakfasts.Add(id, breakfast);
            return false;
        }


    }
}