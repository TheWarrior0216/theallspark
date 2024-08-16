
namespace theallspark.Services;


public class RecipeService
{
  private readonly RecipeRepository _recipeRepository;

  public RecipeService(RecipeRepository recipeRepository)
  {
    _recipeRepository = recipeRepository;
  }

  internal Recipe CreateRecipe(Recipe recipeData)
  {
    Recipe recipe = _recipeRepository.CreateRecipe(recipeData);
    return recipe;
  }
}