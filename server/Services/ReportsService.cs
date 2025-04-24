namespace help.Services;

public class ReportsService
{
  public ReportsService(ReportsRepository repository)
  {
    _repository = repository;
  }
  private readonly ReportsRepository _repository;

}