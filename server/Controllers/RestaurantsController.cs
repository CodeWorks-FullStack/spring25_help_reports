using help.Interfaces;

namespace help.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
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
      Restaurant restaurant = _restaurantsService.Create(restaurantData);
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

  [HttpGet("{restaurantId}")]
  public ActionResult<Restaurant> GetById(int restaurantId)
  {
    try
    {
      Restaurant restaurant = _restaurantsService.GetById(restaurantId);
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
}