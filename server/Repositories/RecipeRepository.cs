namespace theallspark.Repositories;


public class RecipeRepository
{
  private readonly IDbConnection _db;

  public RecipeRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    string sql = @"
Insert Into 
recipe(title, instructions, img, category, creatorId)
VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);

Select
recipe.*,
accounts.*
FROM recipe
JOIN accounts ON accounts.id = recipe.creatorId
WHERE recipe.id = LAST_INSERT_ID();";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, recipeData).FirstOrDefault();
    return recipe;
  }

}