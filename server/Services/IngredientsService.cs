



namespace theallspark.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _ingredientsRepository;

  public IngredientsService(IngredientsRepository ingredientsRepository)
  {
    _ingredientsRepository = ingredientsRepository;
  }

  internal Ingredient CreateIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _ingredientsRepository.CreateIngredient(ingredientData);
    return ingredient;
  }

  internal string DecimateIngredient(int ingredientId)
  {
    // Ingredient ingredient = GetIngredientById(ingredientId);
    _ingredientsRepository.DecimateIngredient(ingredientId);
    return "Decimated the Ingredient";
  }

  internal List<Ingredient> GetIngredientsForRecipe(int recipeId)
  {
    List<Ingredient> ingredients = _ingredientsRepository.GetIngredientsForRecipe(recipeId);
    return ingredients;
  }
  private Ingredient GetIngredientById(int ingredientId)
  {
    Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
    return ingredient;
  }
}
