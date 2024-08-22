namespace theallspark.Controllers;


[ApiController]
[Route("api/ingredients")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth0Provider;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
  {
    _ingredientsService = ingredientsService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      // Account UserInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.CreateIngredient(ingredientData);
      return Ok(ingredient);
    }

    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  public ActionResult<string> DecimateIngredient(int ingredientId)
  {
    try
    {
      string message = _ingredientsService.DecimateIngredient(ingredientId);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
