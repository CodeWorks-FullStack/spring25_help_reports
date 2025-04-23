


using Microsoft.AspNetCore.Http.HttpResults;

namespace help.Services;

public class RestaurantsService
{
  public RestaurantsService(RestaurantsRepository repository)
  {
    _repository = repository;
  }
  private readonly RestaurantsRepository _repository;

  internal Restaurant Create(Restaurant restaurantData)
  {
    Restaurant restaurant = _repository.Create(restaurantData);
    return restaurant;
  }

  internal List<Restaurant> GetAll()
  {
    List<Restaurant> restaurants = _repository.GetAll();
    return restaurants;
  }

  internal Restaurant GetById(int restaurantId)
  {
    Restaurant restaurant = _repository.GetById(restaurantId);

    if (restaurant == null)
    {
      throw new Exception("Invalid id: " + restaurantId);
    }

    return restaurant;
  }

  internal Restaurant Update(int restaurantId, Account userInfo, Restaurant restaurantUpdateData)
  {
    Restaurant restaurant = GetById(restaurantId);

    if (restaurant.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT UPDATE ANOTHER USER'S RESTAURANT, {userInfo.Name.ToUpper()}!!!");
    }

    restaurant.Name = restaurantUpdateData.Name ?? restaurant.Name;
    restaurant.Description = restaurantUpdateData.Description ?? restaurant.Description;
    restaurant.IsShutdown = restaurantUpdateData.IsShutdown ?? restaurant.IsShutdown;

    _repository.Update(restaurant);

    return restaurant;
  }

  internal string Delete(int restaurantId, Account userInfo)
  {
    Restaurant restaurant = GetById(restaurantId);

    if (restaurant.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE ANOTHER USER'S RESTAURANT, {userInfo.Name.ToUpper()}!!!");
    }

    _repository.Delete(restaurantId);

    return restaurant.Name + " has been deleted, big dawg!";
  }
}