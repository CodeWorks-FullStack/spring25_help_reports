using System.ComponentModel.DataAnnotations;

namespace help.Models;

public class Report : RepoItem<int>
{
  public string Title { get; set; }
  [MaxLength(500)] public string Body { get; set; }
  [Range(1, 5)] public int Score { get; set; }
  [Url, MaxLength(1000)] public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int RestaurantId { get; set; }
  public Profile Reporter { get; set; }
}