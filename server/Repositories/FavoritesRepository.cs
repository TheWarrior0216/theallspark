

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
recipe.*,
accounts.*
FROM favorite
JOIN recipe ON favorite.recipeId = recipe.id
JOIN accounts ON accounts.Id = favorite.accountId
WHERE favorite.id = LAST_INSERT_ID();";
    FavoriteRecipe favorite = _db.Query<Favorite, FavoriteRecipe, Account, FavoriteRecipe>(sql, (favorite, recipe, profile) =>
    {
      recipe.FavoriteId = favorite.Id;
      recipe.RecipeId = favorite.RecipeId;
      recipe.Creator = profile;
      return recipe;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }
}
