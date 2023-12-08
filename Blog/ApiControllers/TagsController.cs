using Blog.Data.Models;
using Blog.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {

        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [Route("/AddTags/")]
        [HttpPost]
        public IEnumerable<Tag> AddTags([FromBody] IList<string> tagsName)
        {
            var result = _tagService.Add(tagsName);
            return result;
        }

    }
}
