using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllSpice.Repositories
{
    public class RecipesRepository
    {
        private readonly IDbConnection _db;

        public RecipesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Recipe CreateRecipe(Recipe recipeData)
        {
            string sql = @"
            INSERT INTO recipes
            (title, category, img, instructions, creatorId)
            VALUES
            (@title, @category, @img, @instructions, @creatorId);
            SELECT
            act.*,
            rec.*
            FROM recipes rec
            JOIN accounts act ON act.id = rec.creatorId
            WHERE rec.id = LAST_INSERT_ID()
            ;";
            Recipe newRecipe = _db.Query<Account, Recipe, Recipe>(sql, (account, recipe) =>
            {
                recipe.Creator = account;
                return recipe;
            }, recipeData).FirstOrDefault();
            return newRecipe;
        }

        internal List<Recipe> Get()
        {
            string sql = @"
            SELECT
            rec.*,
            act.*
            FROM recipes rec
            JOIN accounts act ON act.id = rec.creatorId
            ;";

            List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
            {
                recipe.Creator = account;
                return recipe;
            }).ToList();
            return recipes;
        }

        internal Recipe Get(int recipeId)
        {
            string sql = @"
            SELECT
            rec.*,
            act.*
            FROM recipes rec
            JOIN accounts act ON rec.creatorId = act.id
            WHERE rec.id = @recipeId
            ;";
            Recipe foundRecipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
            {
                recipe.Creator = creator;
                return recipe;
            }, new { recipeId }).FirstOrDefault();
            return foundRecipe;
        }

        internal void RemoveRecipe(int recipeId)
        {
            string sql = "DELETE FROM recipes WHERE id = @recipeId";
            int rowsAffected = _db.Execute(sql, new { recipeId });
            if (rowsAffected > 1) throw new Exception("Everything is gone");
            if (rowsAffected < 1) throw new Exception("Nothing is gone");
        }

        internal Recipe UpdateRecipe(Recipe original)
        {
            string sql = @"
            UPDATE recipes
            SET
            title = @title,
            instructions = @instructions,
            img = @img
            WHERE id = @id;
            SELECT * FROM recipes WHERE id = @id
            ;";
            Recipe recipe = _db.Query<Recipe>(sql, original).FirstOrDefault();
            return recipe;
        }
    }
}