namespace help.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
  public ReportsController(Auth0Provider auth0Provider, ReportsService reportsService)
  {
    _auth0Provider = auth0Provider;
    _reportsService = reportsService;
  }
  private readonly Auth0Provider _auth0Provider;
  private readonly ReportsService _reportsService;
}