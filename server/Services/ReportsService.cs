
namespace help.Services;

public class ReportsService
{
  public ReportsService(ReportsRepository repository, RestaurantsService restaurantsService)
  {
    _repository = repository;
    _restaurantsService = restaurantsService;
  }
  private readonly ReportsRepository _repository;
  private readonly RestaurantsService _restaurantsService; // 🧑‍🤝‍🧑services are friends

  internal Report CreateReport(Report reportData, Account userInfo)
  {
    Restaurant restaurant = _restaurantsService.GetById(reportData.RestaurantId, userInfo);

    // if (restaurant.IsShutdown == true) restaurants service already handles this

    if (restaurant.CreatorId == userInfo.Id)
    {
      throw new Exception($"Hey {userInfo.Name}, you are the owner of {restaurant.Name}, and you cannot leave reviews for your own restaurant you filthy animal");
    }

    Report report = _repository.Create(reportData);
    return report;
  }
}