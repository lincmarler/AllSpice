using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Repositories
{
    public class IngredientsRepository
    {
        private readonly IDbConnection _db;
        public IngredientsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Ingredient CreateIngredient(Ingredient ingredientData)
        {
            string sql = @"
            INSERT INTO ingredients
            (name, quantity, recipeId)
            VALUES
            (@name, @quantity, @recipeId);
            SELECT
            *
            FROM ingredients
            WHERE id = LAST_INSERT_ID()
            ;";
            Ingredient newIngredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
            return newIngredient;
        }

        internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            string sql = @"
            SELECT
            *
            FROM ingredients
            WHERE recipeId = @recipeId
            ;";
            List<Ingredient> ingredients = _db.Query<Ingredient>(sql, recipeId).ToList();
            return ingredients;
        }
    }
}