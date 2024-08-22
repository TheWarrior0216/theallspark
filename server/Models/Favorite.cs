namespace theallspark.Models;

public class Favorite
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public int RecipeId { get; set; }
  public Recipe Recipe { get; set; }
  public string AccountId { get; set; }
  public Account Creator { get; set; }
}
public class FavoriteRecipe : Recipe
{
  public int RecipeId { get; set; }
  public int FavoriteId { get; set; }

}