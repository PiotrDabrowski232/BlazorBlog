using Blog.Logic.Dto.PostDtos;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [Route("/sendPost")]
        [HttpPost]
        public IActionResult Post([FromBody] PostApiDto post)
        {
            try
            {
                _postService.Add(post.Post, post.Tags);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("/GetPosts")]
        [HttpGet]
        public IEnumerable<PostDto> GetAllPosts()
        {
            var result = _postService.GetAll();
            return result;
        }


        [Route("/DeletePost")]
        [HttpDelete]
        public IActionResult Delete([FromBody] string id)
        {
            try
            {
                _postService.Delete(Guid.Parse(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("/UpdatePost")]
        [HttpPut]
        public IActionResult Put([FromBody] PostDto post)
        {
            try
            {
                _postService.UpdatePost(post);
                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("/GetUserPosts")]
        [HttpGet]
        public ActionResult<Task<IEnumerable<PostDto>>> GetAllUserPosts([FromBody] string userEmail)
        {
            var result = _postService.GetAllEditableAndDeletableByUser(userEmail);
            return result;
        }


        [Route("/GetPost")]
        [HttpGet]
        public ActionResult<PostDto> GetByPostId([FromQuery] string id)
        {
            var result = _postService.GetByPostId<PostDto>(id);
            return result;
        }
    }
}
