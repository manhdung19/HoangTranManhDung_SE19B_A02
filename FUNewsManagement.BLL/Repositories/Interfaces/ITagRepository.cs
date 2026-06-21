using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Interfaces
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
    }
}
