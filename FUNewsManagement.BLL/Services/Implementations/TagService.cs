using FUNewsManagement.BLL.Repositories.Implementations;
using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService()
        {
            _tagRepository = new TagRepository();
        }

        public List<Tag> GetAllTags() => _tagRepository.GetAllTags();
    }
}
