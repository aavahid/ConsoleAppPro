using System;
using Domain.Models;
using Repository.Repositories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService()
        {
            _restaurantRepository = new RestaurantRepository();
        }

        public void Create(Restaurant restaurant)
        {
            _restaurantRepository.Create(restaurant);
        }

        public List<Restaurant> GetAll()
        {
            return _restaurantRepository.GetAll();
        }
    }
}

