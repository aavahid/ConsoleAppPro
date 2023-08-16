using System;
using Domain.Models;

namespace Service.Services.Interfaces
{
    public interface IRestaurantService
    {
        void Create(Restaurant restaurant);
        List<Restaurant> GetAll();
    }
}

