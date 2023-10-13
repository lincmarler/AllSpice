using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

namespace AllSpice.Controllers;

[ApiController]
[Route("api/recipes")]
public class RecipesController : ControllerBase
{
    private readonly RecipesService _recipesService;
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0, IngredientsService ingredientsService)
    {
        _recipesService = recipesService;
        _auth0 = auth0;
        _ingredientsService = ingredientsService;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> Get()
    {
        try
        {
            List<Recipe> recipes = _recipesService.Get();
            return Ok(recipes);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [HttpGet("{recipeId}")]
    public ActionResult<Recipe> GetById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.Get(recipeId);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Recipe>> CreateRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
            recipeData.CreatorId = userInfo.Id;
            Recipe newRecipe = _recipesService.CreateRecipe(recipeData);
            return Ok(newRecipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [Authorize]
    [HttpPut("{recipeId}")]
    public ActionResult<Recipe> UpdateRecipe([FromBody] Recipe updateData, int recipeId)
    {
        try
        {
            updateData.Id = recipeId;
            Recipe recipe = _recipesService.UpdateRecipe(updateData);
            return Ok(recipe);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);

        }
    }

    [Authorize]
    [HttpDelete("{recipeId}")]
    public ActionResult<string> RemoveRecipe(int recipeId)
    {
        try
        {
            string message = _recipesService.RemoveRecipe(recipeId);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{recipeId}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientsByRecipeId(int recipeId)
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipeId(recipeId);
            return Ok(ingredients);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
