using Microsoft.AspNetCore.Http.HttpResults;

namespace theallspark.Controllers;



[ApiController]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{

  private readonly Auth0Provider _auth0provider;
  private readonly RecipeService _recipeService;
  private readonly IngredientsService _ingredientsService;

  public RecipesController(Auth0Provider auth0provider, RecipeService recipeService, IngredientsService ingredientsService)
  {
    _auth0provider = auth0provider;
    _recipeService = recipeService;
    _ingredientsService = ingredientsService;
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
  [HttpGet]
  public ActionResult<List<Recipe>> GetAllRecipes()
  {
    try
    {
      List<Recipe> recipes = _recipeService.GetAllRecipes();
      return Ok(recipes);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetRecipeById(int recipeId)
  {
    try
    {
      Recipe recipe = _recipeService.GetRecipeById(recipeId);
      return Ok(recipe);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpPut("{recicpeId}")]
  [Authorize]
  public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData, int recicpeId)
  {
    try
    {
      Account UserInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      Recipe recipe = _recipeService.UpdateRecipe(recicpeId, UserInfo.Id, recipeData);
      return Ok(recipe);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
  [HttpDelete("{recipeId}")]
  [Authorize]
  public async Task<ActionResult<string>> DescimateRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipeService.DescimateRecipe(recipeId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }

  }
  [HttpGet("{recipeId}/ingredients")]
  public ActionResult<List<Ingredient>> GetIngredientsForRecipe(int recipeId)
  {
    try
    {
      List<Ingredient> ingredients = _ingredientsService.GetIngredientsForRecipe(recipeId);
      return ingredients;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}