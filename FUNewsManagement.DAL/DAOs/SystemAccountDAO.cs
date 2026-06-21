using FUNewsManagement.DAL.Models; // Hãy đảm bảo namespace này trùng với thư mục Models chứa DbContext của bạn
using System;
using System.Linq;

namespace FUNewsManagement.DAL.DAOs
{
    public class SystemAccountDAO
    {
        // 1. Khai báo biến static lưu trữ thực thể duy nhất của DAO
        private static SystemAccountDAO _instance = null;

        // Biến lock dùng để bảo mật đa luồng (Thread-safe Singleton)
        private static readonly object _instanceLock = new object();

        // 2. Private Constructor: Ngăn chặn việc tạo thực thể tự do từ bên ngoài bằng lệnh 'new'
        private SystemAccountDAO() { }

        // 3. Property công khai để các tầng trên (Repository) có thể truy cập vào thực thể duy nhất
        public static SystemAccountDAO Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new SystemAccountDAO();
                    }
                    return _instance;
                }
            }
        }

        /// <summary>
        /// Hàm lấy thông tin tài khoản dựa vào Email và Password (Phục vụ chức năng Đăng nhập)
        /// </summary>
        public SystemAccount GetAccountByEmailAndPassword(string email, string password)
        {
            try
            {
                // Khởi tạo DbContext để làm việc với Database
                using var contextAccount = new FunewsManagementContext();

                // Sử dụng LINQ để tìm tài khoản khớp cả Email và Password
                return contextAccount.SystemAccounts
                    .FirstOrDefault(a => a.AccountEmail.Equals(email) && a.AccountPassword.Equals(password));
            }
            catch (Exception ex)
            {
                // Ném lỗi ra ngoài nếu quá trình kết nối DB gặp sự cố
                throw new Exception("Lỗi khi truy vấn tài khoản từ Database: " + ex.Message);
            }
        }

        public List<SystemAccount> GetSystemAccounts()
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.ToList();
        }

        public SystemAccount GetSystemAccountById(short id)
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(c => c.AccountId == id);
        }

        public void AddSystemAccount(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Add(account);
            context.SaveChanges();
        }

        public void UpdateSystemAccount(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Update(account);
            context.SaveChanges();
        }

        public void DeleteSystemAccount(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.SystemAccounts.Remove(account);
            context.SaveChanges();
        }

    }
}