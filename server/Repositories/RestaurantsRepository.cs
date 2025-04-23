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
    throw new NotImplementedException();
  }

  public Restaurant Create(Restaurant data)
  {
    string sql = @"
    INSERT INTO
    restaurants(name, description, img_url, creator_id)
    VALUES(@Name, @Description, @ImgUrl, @CreatorId);
    
    SELECT * FROM restaurants WHERE id = LAST_INSERT_ID();";

    Restaurant restaurant = _db.Query<Restaurant>(sql, data).SingleOrDefault();
    return restaurant;
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