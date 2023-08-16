using System;
using Domain.Models;
using Repository.Repositories;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private static int _count = 1;
        public RestaurantService()
        {
            _restaurantRepository = new RestaurantRepository();
        }

        public void Create(Restaurant restaurant)
        {
            restaurant.Id = _count;
            _restaurantRepository.Create(restaurant);
            _count++;
        }

        public List<Restaurant> GetAll()
        {
            return _restaurantRepository.GetAll();
        }
    }
}

