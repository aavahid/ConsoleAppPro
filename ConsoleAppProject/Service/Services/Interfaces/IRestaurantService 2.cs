using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IRestaurantService
    {
        void Create(Restaurant restaurant);
        Restaurant GetById(int restaurantId);
        List<Restaurant> GetAll();
        void Edit(Restaurant restaurant);
        void Delete(Restaurant restaurantToDelete);

    }
}