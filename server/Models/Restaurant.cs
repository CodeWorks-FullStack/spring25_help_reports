using System.ComponentModel.DataAnnotations;

namespace help.Models;

public class Restaurant : RepoItem<int>
{
  public string Name { get; set; }
  [Url, MaxLength(1000)] public string ImgUrl { get; set; }
  [MaxLength(3000)] public string Description { get; set; }
  public int Visits { get; set; }
  public bool? IsShutdown { get; set; }
  public string CreatorId { get; set; }
  public Profile Owner { get; set; }
}
