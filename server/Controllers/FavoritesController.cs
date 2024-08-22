namespace theallspark.Controllers;

[ApiController]
[Route("api/favorites")]
public class FavoritesController : ControllerBase
{
  private readonly Auth0Provider _auth0Provider;
  private readonly FavoritesService _FavoritesService;

  public FavoritesController(Auth0Provider auth0Provider, FavoritesService favoritesService)
  {
    _auth0Provider = auth0Provider;
    _FavoritesService = favoritesService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<FavoriteRecipe>> CreateFavoriteRecipe([FromBody] Favorite favoriteData)
  {
    try
    {
      Account UserInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = UserInfo.Id;
      FavoriteRecipe favorite = _FavoritesService.CreateFavoriteRecipe(favoriteData);
      return Ok(favorite);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}
