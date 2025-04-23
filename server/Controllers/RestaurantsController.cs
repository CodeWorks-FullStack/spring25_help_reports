using help.Interfaces;

namespace help.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase, IController<Restaurant>
{
  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth0Provider)
  {
    _restaurantsService = restaurantsService;
    _auth0Provider = auth0Provider;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly RestaurantsService _restaurantsService;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurantData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      restaurantData.CreatorId = userInfo.Id;
      Restaurant restaurant = _restaurantsService.CreateRestaurant(restaurantData);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpGet]
  public ActionResult<List<Restaurant>> GetAll()
  {
    try
    {
      List<Restaurant> restaurants = _restaurantsService.GetAll();
      return Ok(restaurants);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  public ActionResult<Restaurant> GetById(int id)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<Restaurant>> Update(int id, [FromBody] Restaurant updateTData)
  {
    throw new NotImplementedException();
  }

  public Task<ActionResult<string>> Delete(int id)
  {
    throw new NotImplementedException();
  }
}