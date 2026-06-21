using FUNewsManagement.DAL.Models;

namespace FUNewsManagement.BLL.Services.Interfaces
{
    public interface ISystemAccountService
    {
        // Hàm xử lý logic đăng nhập tổng thể, trả về đối tượng tài khoản kèm theo vai trò
        SystemAccount Authenticate(string email, string password);
        List<SystemAccount> GetSystemAccounts();
        SystemAccount GetSystemAccountById(short id);
        void AddSystemAccount(SystemAccount account);
        void UpdateSystemAccount(SystemAccount account);
        void DeleteSystemAccount(SystemAccount account);

    }
}