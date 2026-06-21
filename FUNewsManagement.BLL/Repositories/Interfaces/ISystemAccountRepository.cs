using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Repositories.Interfaces
{
    public interface ISystemAccountRepository
    {
        // Định nghĩa hàm ký mẫu cho việc lấy tài khoản để đăng nhập
        SystemAccount Login(string email, string password);
        List<SystemAccount> GetSystemAccounts();
        SystemAccount GetSystemAccountById(short id);
        void AddSystemAccount(SystemAccount account);
        void UpdateSystemAccount(SystemAccount account);
        void DeleteSystemAccount(SystemAccount account);
    }
}