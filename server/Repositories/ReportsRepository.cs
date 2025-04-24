using help.Interfaces;

namespace help.Repositories;

public class ReportsRepository : IRepository<Report>
{
  public ReportsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public List<Report> GetAll()
  {
    throw new NotImplementedException();
  }

  public Report Create(Report reportData)
  {
    string sql = @"
    INSERT INTO
    reports(title, score, img_url, body, creator_id, restaurant_id)
    VALUES(@Title, @Score, @ImgUrl, @Body, @CreatorId, @RestaurantId);

    SELECT
    reports.*,
    accounts.*
    FROM reports
    INNER JOIN accounts ON accounts.id = reports.creator_id
    WHERE reports.id = LAST_INSERT_ID();";

    Report createdReport = _db.Query(sql, (Report report, Profile account) =>
    {
      report.Reporter = account;
      return report;
    }, reportData).SingleOrDefault();
    return createdReport;
  }

  public Report GetById(int id)
  {
    throw new NotImplementedException();
  }

  public void Update(Report data)
  {
    throw new NotImplementedException();
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  internal List<Report> GetReportsByRestaurantId(int restaurantId)
  {
    string sql = @"
    SELECT
    reports.*,
    accounts.*
    FROM reports
    INNER JOIN accounts ON accounts.id = reports.creator_id
    WHERE reports.restaurant_id = @restaurantId
    ORDER BY reports.created_at ASC;";

    List<Report> reports = _db.Query(sql, (Report report, Profile account) =>
    {
      report.Reporter = account;
      return report;
    }, new { restaurantId }).ToList();

    return reports;
  }
}