using System;
using Domain.Models;
using Repository.Data;

namespace Repository.Repositories
{
    public class RestaurantRepository : BaseRepository<Restaurant> , IRestaurantRepository
    {
        public void DeleteRestaurant(int id)
        {
            Restaurant locationToRemove = AppDbContext<Restaurant>.datas.Find(l => l.Id == id);
            if (locationToRemove != null)
            {
                AppDbContext<Restaurant>.datas.Remove(locationToRemove);
            }
        }
        public void EditRestaurant(Restaurant restaurant)
        {
            Restaurant existingRestaurant = AppDbContext<Restaurant>.datas.Find(l => l.Id == restaurant.Id);
            if (existingRestaurant != null)
            {
                existingRestaurant.Title = restaurant.Title;
                existingRestaurant.Description = restaurant.Description;
                existingRestaurant.Location = restaurant.Location;
                

            }
        }
    }
}

