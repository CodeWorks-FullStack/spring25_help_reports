using help.Interfaces;

namespace help.Repositories;

public class RestaurantsRepository : IRepository<Restaurant>
{
  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public List<Restaurant> GetAll()
  {
    string sql = @"
    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id;";

    List<Restaurant> restaurants = _db.Query(sql, (Restaurant restaurant, Profile account) =>
    {
      restaurant.Owner = account;
      return restaurant;
    }).ToList();


    return restaurants;
  }

  public Restaurant Create(Restaurant data)
  {
    string sql = @"
    INSERT INTO
    restaurants(name, description, img_url, creator_id)
    VALUES(@Name, @Description, @ImgUrl, @CreatorId);
    
    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    WHERE restaurants.id = LAST_INSERT_ID();";

    Restaurant createdRestaurant = _db.Query(sql, (Restaurant restaurant, Profile account) =>
    {
      restaurant.Owner = account;
      return restaurant;
    }, data).SingleOrDefault();

    return createdRestaurant;
  }

  public Restaurant GetById(int id)
  {
    throw new NotImplementedException();
  }

  public void Update(Restaurant data)
  {
    throw new NotImplementedException();
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }
}