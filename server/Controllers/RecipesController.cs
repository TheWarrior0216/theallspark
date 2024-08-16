namespace theallspark.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{

  private readonly Auth0Provider _auth0provider;
  private readonly RecipeService _recipeService;

  public AlbumsController(Auth0Provider auth0provider, RecipeService recipeService)
  {
    _auth0provider = auth0provider;
    _recipeService = recipeService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipeService.CreateRecipe(recipeData);
      return recipe;
    }
    catch (Exception exception)
    {

      return BadRequest(exception.Message);
    }
  }

}