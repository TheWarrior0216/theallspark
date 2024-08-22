



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

  internal void DescimateRecipe(int recipeId)
  {
    string sql = "DELETE FROM recipe WHERE id = @recipeId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { recipeId });

    if (rowsAffected == 0)
    {
      throw new Exception("You didn't do anything, Sorry");
    }
    if (rowsAffected >= 2)
    {
      throw new Exception("You Broke it...");
    }
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
      SELECT
      recipe.*,
      accounts.*
      FROM recipe
      JOIN accounts ON accounts.id = recipe.creatorId
      ;";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
      SELECT
      recipe.*,
      accounts.*
      From recipe
      JOIN accounts ON accounts.id = recipe.creatorId
      WHERE recipe.id = @recipeId
      ;";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, profile) =>
    {
      recipe.Creator = profile;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe recipeToUpdate)
  {
    string sql = @"
        UPDATE recipe
        SET
        title = @Title,
        instructions = @Instructions
        WHERE id = @Id;

        SELECT
      recipe.*,
      accounts.*
      From recipe
      JOIN accounts ON accounts.id = recipe.creatorId
      WHERE recipe.id = @Id;";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, JoinCreator, recipeToUpdate).FirstOrDefault();
    return recipe;
  }

  private Recipe JoinCreator(Recipe recipe, Account profile)
  {
    recipe.Creator = profile;
    return recipe;
  }
}