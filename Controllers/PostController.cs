using blog.Extensions;
using blog.ViewModels;
using blog.ViewModels.Posts;
using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace blog.Controllers
{
    public class PostController : ControllerBase
    {
        [HttpGet("v1/Posts")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x=> x.Category)
                    .Include(x=> x.Author)
                    .Select(x => new ListPostViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name} - ({x.Author.Email})"
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Post>(error: "05xe08 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/Posts/{id:int}")]
        public async Task<IActionResult> GetAsync(
            [FromServices] BlogDataContext context,
            [FromRoute] int id)
        {
            try
            {
                var post = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Author)
                    .ThenInclude(x => x.Roles)
                    .Include(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (post == null)
                    return NotFound(new ResultViewModel<Post>("Erro 0xl1 - Conteúdo não encotnrado"));

                return Ok(post);
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Post>(error: "05xe08 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/Posts/category/{category}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] string category,
            [FromServices] BlogDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Posts.AsNoTracking().CountAsync();
                var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .Include(x => x.Author)
                    .Where(x=> x.Category.Slug == category)
                    .Select(x => new ListPostViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Slug = x.Slug,
                        LastUpdateDate = x.LastUpdateDate,
                        Category = x.Category.Name,
                        Author = $"{x.Author.Name} - ({x.Author.Email})"
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    posts
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Post>(error: "05xe08 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/posts/{id:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id,
            [FromServices] BlogDataContext context)
        {
            try
            {
                var post = await context
                     .Posts
                     .FirstOrDefaultAsync(x => x.Id == id);
                if (post == null)
                    return NotFound(new ResultViewModel<Post>("Item não encontrado!"));

                context.Posts.Remove(post);
                await context.SaveChangesAsync();
                return Ok(new ResultViewModel<Post>(post));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("05xe15 - Não foi possível excluir a categoria"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<Post>("05xe16 - Falha interna no servidor"));
            }
        }

    }
}
