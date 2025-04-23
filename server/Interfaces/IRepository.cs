namespace help.Interfaces;

// NOTE describes what methods a class should implement, but does not provide the implementation (contract)
public interface IRepository<T>
{
  public List<T> GetAll();
  public T Create(T data);
  public T GetById(int id);
  public void Update(T data);
  public void Delete(int id);
}