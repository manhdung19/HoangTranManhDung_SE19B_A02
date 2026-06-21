using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Implementations
{
    public class TagRepository : ITagRepository
    {
        public List<Tag> GetAllTags() => TagDAO.Instance.GetAllTags();
    }
}
