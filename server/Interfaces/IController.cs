namespace help.Interfaces;

public interface IController<T>
{
  [HttpGet]
  public ActionResult<List<T>> GetAll();
  [HttpGet("{id}")]
  public ActionResult<T> GetById(int id);
  [HttpPost]
  [Authorize]
  public Task<ActionResult<T>> Create([FromBody] T tData);
  [HttpPut("{id}")]
  [Authorize]
  public Task<ActionResult<T>> Update(int id, [FromBody] T updateTData);
  [HttpDelete("{id}")]
  [Authorize]
  public Task<ActionResult<string>> Delete(int id);

}