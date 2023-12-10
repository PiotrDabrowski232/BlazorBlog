namespace Blog.Logic.Dto.PostDtos
{
    public class FindPostApiDto
    {
        public string? PostName { get; set; }
        public IList<string>? TagsName { get; set; }
    }
}
