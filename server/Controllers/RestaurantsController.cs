using help.Interfaces;

namespace help.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth0Provider, ReportsService reportsService)
  {
    _restaurantsService = restaurantsService;
    _auth0Provider = auth0Provider;
    _reportsService = reportsService;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly RestaurantsService _restaurantsService;
  private readonly ReportsService _reportsService;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurantData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      restaurantData.CreatorId = userInfo.Id;
      Restaurant restaurant = _restaurantsService.Create(restaurantData);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE not an authorized route
  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetAll()
  {
    try
    {
      // NOTE you can still check to see who is logged in without authorizing the route
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Restaurant> restaurants = _restaurantsService.GetAll(userInfo);
      return Ok(restaurants);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE not an authorized route
  [HttpGet("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> GetById(int restaurantId)
  {
    try
    {
      // NOTE you can still check to see who is logged in without authorizing the route
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _restaurantsService.GetById(restaurantId, userInfo);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [Authorize]
  [HttpPut("{restaurantId}")]
  public async Task<ActionResult<Restaurant>> Update(int restaurantId, [FromBody] Restaurant restaurantUpdateData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _restaurantsService.Update(restaurantId, userInfo, restaurantUpdateData);
      return Ok(restaurant);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [Authorize]
  [HttpDelete("{restaurantId}")]
  public async Task<ActionResult<string>> Delete(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _restaurantsService.Delete(restaurantId, userInfo);
      return Ok(message);
    }
    catch (Exception exception)
    {
      // if (exception.Message.Contains("ANOTHER USER'S"))
      // {
      //   return Forbid();
      // }

      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{restaurantId}/reports")]
  public async Task<ActionResult<List<Report>>> GetReportsByRestaurantId(int restaurantId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Report> reports = _reportsService.GetReportsByRestaurantId(restaurantId, userInfo);
      return Ok(reports);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}