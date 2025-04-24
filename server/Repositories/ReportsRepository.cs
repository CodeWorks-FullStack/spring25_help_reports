namespace help.Repositories;

public class ReportsRepository
{
  public ReportsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

}