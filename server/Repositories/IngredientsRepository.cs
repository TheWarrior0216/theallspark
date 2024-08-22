
namespace theallspark.Repositories;
public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    string sql = @"
      INSERT INTO

      ingredient(name, quantity, recipeId)
      VALUES(@Name, @Quantity, @RecipeId);

      SELECT
      *
      FROM
      ingredient
      WHERE id = LAST_INSERT_ID();";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal void DecimateIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredient WHERE id = @ingredientId;";
    int rowsAffected = _db.Execute(sql, new { ingredientId });

    if (rowsAffected == 0)
    {
      throw new Exception("You didn't do anything, Sorry");
    }
    if (rowsAffected >= 2)
    {
      throw new Exception("You Broke it...");
    }
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = @"
      SELECT 
      * 
      FROM
      ingredient
      WHERE Id = @ingredientId;";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    string sql = @"
      SELECT 
      * 
      FROM
      ingredient
      WHERE ingredient.recipeId = @recipeId;";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }
}
