using FUNewsManagement.BLL.Repositories.Implementations;
using FUNewsManagement.BLL.Repositories.Interfaces;
using FUNewsManagement.BLL.Services.Interfaces;
using FUNewsManagement.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace FUNewsManagement.BLL.Services.Implementations
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        // Constructor nhận vào IConfiguration để đọc file appsettings.json
        public SystemAccountService(IConfiguration configuration)
        {
            _accountRepository = new SystemAccountRepository();
            _configuration = configuration;
        }

        public SystemAccount Authenticate(string email, string password)
        {
            // 1. Đọc thông tin Admin mặc định từ file appsettings.json
            string adminEmail = _configuration["AdminAccount:Email"];
            string adminPassword = _configuration["AdminAccount:Password"];

            // 2. Kiểm tra xem có phải tài khoản Admin đang đăng nhập không
            if (email == adminEmail && password == adminPassword)
            {
                // Nếu đúng là Admin, tự khởi tạo đối tượng SystemAccount với Role đặc biệt (ví dụ: 0 hoặc -1 tùy bạn quy ước)
                return new SystemAccount
                {
                    AccountEmail = adminEmail,
                    AccountName = "System Administrator",
                    AccountRole = 0 // Quy ước Role = 0 là Admin hệ thống
                };
            }

            // 3. Nếu không phải Admin, gọi xuống tầng Repository để tìm kiếm Staff (Role=1) hoặc Lecturer (Role=2) trong DB
            return _accountRepository.Login(email, password);
        }

        public List<SystemAccount> GetSystemAccounts() => _accountRepository.GetSystemAccounts();
        public SystemAccount GetSystemAccountById(short id) => _accountRepository.GetSystemAccountById(id);
        public void AddSystemAccount(SystemAccount account) => _accountRepository.AddSystemAccount(account);
        public void UpdateSystemAccount(SystemAccount account) => _accountRepository.UpdateSystemAccount(account);
        public void DeleteSystemAccount(SystemAccount account) => _accountRepository.DeleteSystemAccount(account);

    }
}