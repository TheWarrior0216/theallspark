

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

  internal string DescimateRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("Wrong Account Bucko");
    }
    _recipeRepository.DescimateRecipe(recipeId);
    return "This Recipe Has been decimated!!";
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _recipeRepository.GetAllRecipes();
    return recipes;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _recipeRepository.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception("This Id Does Not Exist");
    }
    return recipe;
  }

  internal Recipe UpdateRecipe(int recicpeId, string userId, Recipe recipeData)
  {
    Recipe recipeToUpdate = GetRecipeById(recicpeId);
    if (recipeToUpdate.CreatorId != userId)
    {
      throw new Exception("Umm Who Are You?? This Aint Yours???");
    }
    recipeToUpdate.Title = recipeData.Title;
    recipeToUpdate.Instructions = recipeData.Instructions;
    Recipe recipe = _recipeRepository.UpdateRecipe(recipeToUpdate);
    return recipe;
  }
}