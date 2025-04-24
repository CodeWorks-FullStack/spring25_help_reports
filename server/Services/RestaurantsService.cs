
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

  private List<Restaurant> GetAll()
  {
    List<Restaurant> restaurants = _repository.GetAll();
    return restaurants;
  }

  internal List<Restaurant> GetAll(Account userInfo)
  {
    List<Restaurant> restaurants = GetAll();
    // NOTE only give restaurants that are not shutdown OR I am the owner of
    return restaurants.FindAll(restaurant => restaurant.IsShutdown == false || restaurant.CreatorId == userInfo?.Id);
  }

  private Restaurant GetById(int restaurantId)
  {
    Restaurant restaurant = _repository.GetById(restaurantId);

    if (restaurant == null)
    {
      throw new Exception("Invalid id: " + restaurantId);
    }

    return restaurant;
  }

  internal Restaurant GetById(int restaurantId, Account userInfo)
  {
    // NOTE overloads can still call each other
    Restaurant restaurant = GetById(restaurantId);

    // NOTE userInfo will be null here if you are not logged in! Make sure your use a null check OR elvis operator
    if (restaurant.IsShutdown == true && restaurant.CreatorId != userInfo?.Id)
    {
      throw new Exception($"Invalid id: {restaurantId} 😉");
    }

    return restaurant;
  }

  internal Restaurant IncreaseVisits(int restaurantId, Account userInfo)
  {
    Restaurant restaurant = GetById(restaurantId, userInfo);

    if (restaurant.IsShutdown == true)
    {
      return restaurant;
    }

    // NOTE this works
    // restaurant.Visits++;
    // _repository.IncreaseVisits(restaurant);

    restaurant.Visits = _repository.IncrementVisits(restaurant.Id);

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