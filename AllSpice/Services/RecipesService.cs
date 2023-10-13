using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Services
{
    public class RecipesService
    {
        private readonly RecipesRepository _repo;

        public RecipesService(RecipesRepository repo)
        {
            _repo = repo;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            Recipe newRecipe = _repo.CreateRecipe(recipeData);
            return newRecipe;
        }

        internal List<Recipe> Get()
        {
            List<Recipe> recipes = _repo.Get();
            return recipes;
        }

        internal Recipe Get(int recipeId)
        {
            Recipe foundRecipe = _repo.Get(recipeId);
            if (foundRecipe == null) throw new Exception($"Unable to find recipe at {recipeId}");
            return foundRecipe;
        }

        internal string RemoveRecipe(int recipeId)
        {
            Recipe recipe = this.Get(recipeId);
            _repo.RemoveRecipe(recipeId);
            return $"{recipe.Title} was removed";
        }

        internal Recipe UpdateRecipe(Recipe updateData)
        {
            Recipe original = this.Get(updateData.Id);
            original.Title = updateData.Title != null ? updateData.Title : original.Title;
            original.Instructions = updateData.Instructions ?? updateData.Instructions;
            original.Img = updateData.Img ?? original.Img;
            Recipe recipe = _repo.UpdateRecipe(original);
            return recipe;
        }
    }
}