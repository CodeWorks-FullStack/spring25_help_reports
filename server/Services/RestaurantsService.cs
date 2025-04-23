


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
}