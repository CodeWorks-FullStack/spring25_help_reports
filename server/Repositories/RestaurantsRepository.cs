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
    COUNT(reports.id) AS report_count,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    LEFT OUTER JOIN reports ON reports.restaurant_id = restaurants.id
    GROUP BY restaurants.id
    ORDER BY restaurants.created_at ASC;";

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
    string sql = @"
    SELECT
    restaurants.*,
    accounts.*
    FROM restaurants
    INNER JOIN accounts ON accounts.id = restaurants.creator_id
    WHERE restaurants.id = @id;";

    Restaurant foundRestaurant = _db.Query(sql, (Restaurant restaurant, Profile account) =>
    {
      restaurant.Owner = account;
      return restaurant;
    }, new { id }).SingleOrDefault();

    return foundRestaurant;
  }

  public void Update(Restaurant updateData)
  {
    string sql = @"
    UPDATE restaurants
    SET
    name = @Name,
    description = @Description,
    is_shutdown = @IsShutdown
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, updateData);

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " ROWS WERE UPDATED AND THAT IS NOT GOOD");
    }
  }

  public void Delete(int id)
  {
    string sql = "DELETE FROM restaurants WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id });


    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " ROWS WERE DELETED AND THAT IS NOT GOOD");
    }
  }

  internal void IncreaseVisits(Restaurant restaurant)
  {
    string sql = @"
    UPDATE restaurants
    SET visits = @Visits
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, restaurant);

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " rows were affected and that is bad");
    }
  }

  internal int IncrementVisits(int restaurantId)
  {
    string sql = @"
    UPDATE restaurants
    SET visits = visits + 1
    WHERE id = @restaurantId LIMIT 1;
    
    SELECT visits FROM restaurants WHERE id = @restaurantId LIMIT 1;";

    int visits = _db.ExecuteScalar<int>(sql, new { restaurantId });

    return visits;
  }
}