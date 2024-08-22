


namespace theallspark.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _favoritesRepository;

  public FavoritesService(FavoritesRepository favoritesRepository)
  {
    _favoritesRepository = favoritesRepository;
  }

  internal FavoriteRecipe CreateFavoriteRecipe(Favorite favoriteData)
  {
    FavoriteRecipe favorite = _favoritesRepository.CreateFavoriteRecipe(favoriteData);
    return favorite;
  }
}
