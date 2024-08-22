

namespace theallspark.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
  {
    string sql = @"
      INSERT INTO
      favorite(recipeId, accountId)
      VALUES(@RecipeId, @accountId);

SELECT
favorite.*, 
recipe.*
FROM favorite
JOIN recipe ON favorite.recipeId = recipe.id
WHERE favorite.id = LAST_INSERT_ID();";
    FavoriteRecipe favorite = _db.Query<Favorite, FavoriteRecipe, FavoriteRecipe>(sql, (favorite, recipe) =>
    {
      recipe.FavoriteId = favorite.Id;
      recipe.RecipeId = favorite.RecipeId;
      return recipe;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }
}
