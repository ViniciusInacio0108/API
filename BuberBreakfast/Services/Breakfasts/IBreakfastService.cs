namespace BuberBreakfast.Services.Breakfast;
using BuberBreakfast.Models;

public interface IBreakfastService
{
    void CreateBreakfast(Breakfast breakfast);

    Breakfast GetBreakfast(Guid id);

    Boolean UpsertBreakfast(Breakfast breakfast, Guid id);

    void DeleteBreakfast(Guid id);

    List<Breakfast> AllBreakfasts();
}