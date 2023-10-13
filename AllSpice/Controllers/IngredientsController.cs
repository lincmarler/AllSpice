using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;
        private readonly Auth0Provider _auth0;
        public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0)
        {
            _ingredientsService = ingredientsService;
            _auth0 = auth0;
        }

        [Authorize]
        [HttpPost]
        public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient ingredientData)
        {
            try
            {
                Ingredient newIngredient = _ingredientsService.CreateIngredient(ingredientData);
                return Ok(newIngredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}