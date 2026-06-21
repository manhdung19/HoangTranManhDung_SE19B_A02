using FUNewsManagement.DAL.Models;
namespace FUNewsManagement.DAL.DAOs
{
    public class TagDAO
    {
        private static TagDAO _instance = null;
        private static readonly object _instanceLock = new object();
        private TagDAO() { }
        public static TagDAO Instance
        {
            get { lock (_instanceLock) { if (_instance == null) _instance = new TagDAO(); return _instance; } }
        }
        public List<Tag> GetAllTags()
        {
            using var context = new FunewsManagementContext();
            return context.Tags.ToList();
        }
    }
}
