using blog.Extensions;
using blog.ViewModels;
using blog.ViewModels.Categories;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace blog.Controllers
{
    public class CategoryController : ControllerBase
    {

        //GET Genérico
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories.ToListAsync();
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<List<Category>>(error: "05xe08 - Falha interna no servidor"));
            }
        }

        //Get por ID
        [HttpGet("v1/categories/{Id:int}")]
        public async Task<IActionResult> GetByIdAsync(
       [FromRoute] int id,
       [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                    .Categories
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>(error: "Item solicitado não encontrado"));

                return Ok(new ResultViewModel<Category>(category));
            }
            
            catch
            {
                return StatusCode(500, new ResultViewModel<Category>(error: "05xe10 - Falha interna no servidor"));
            }
        }

        //POST Genérico
        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCategoryViewModels model,
            [FromServices] BlogDataContext context)

        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };
                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/model/{category.Id}", new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe11 - Não foi possível incluir a categoria"));
            }
            catch 
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe12 - Falha interna no servidor"));
            }
        }

        //PUT Genérico
        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int id,
            [FromBody] EditorCategoryViewModels model,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                     .Categories
                     .FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Conteúdo não encontrado!"));

                category.Name = model.Name;
                category.Slug = model.Slug;


                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe13 - Não foi possível atualizar a categoria"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe14 - Falha interna no servidor"));
            }
        }

        //DELETE Genérico
        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var category = await context
                     .Categories
                     .FirstOrDefaultAsync(x => x.Id == id);
                if (category == null)
                    return NotFound(new ResultViewModel<Category>("Item não encontrado!"));

                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Category>(category));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe15 - Não foi possível excluir a categoria"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Category>("05xe16 - Falha interna no servidor"));
            }
        }
    }
}
