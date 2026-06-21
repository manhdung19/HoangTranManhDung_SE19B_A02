using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.DAL.DAOs;
using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Implementations
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        // Triển khai hàm Login bằng cách gọi thực thể duy nhất của DAO thông qua Singleton Pattern
        public SystemAccount Login(string email, string password)
            => SystemAccountDAO.Instance.GetAccountByEmailAndPassword(email, password);

        public List<SystemAccount> GetSystemAccounts() => SystemAccountDAO.Instance.GetSystemAccounts();
        public SystemAccount GetSystemAccountById(short id) => SystemAccountDAO.Instance.GetSystemAccountById(id);
        public void AddSystemAccount(SystemAccount account) => SystemAccountDAO.Instance.AddSystemAccount(account);
        public void UpdateSystemAccount(SystemAccount account) => SystemAccountDAO.Instance.UpdateSystemAccount(account);
        public void DeleteSystemAccount(SystemAccount account) => SystemAccountDAO.Instance.DeleteSystemAccount(account);

    }
}