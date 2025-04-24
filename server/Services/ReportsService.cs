
namespace help.Services;

public class ReportsService
{
  public ReportsService(ReportsRepository repository)
  {
    _repository = repository;
  }
  private readonly ReportsRepository _repository;

  internal Report CreateReport(Report reportData)
  {
    Report report = _repository.Create(reportData);
    return report;
  }
}