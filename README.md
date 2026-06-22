# FUNewsManagementSystem - Assignment 2

Đây là hệ thống quản lý tin tức **(News Management System)** được xây dựng dựa trên kiến trúc **ASP.NET Core Razor Pages**, kết hợp giao tiếp thời gian thực với **SignalR**. Dự án tuân thủ nghiêm ngặt mô hình 3 lớp (3-Tier Architecture) và các pattern Design cơ bản (Repository, Singleton).

Dự án thuộc khuôn khổ bài tập **Assignment 2 - môn PRN222**.

## 📌 Tính năng nổi bật đã hoàn thành (100% Khớp yêu cầu Ass2)

1. **Công nghệ cốt lõi:**
   - **ASP.NET Core Razor Pages** thay vì MVC.
   - Database truy xuất thông qua **Entity Framework Core** (EF Core).
   - **SignalR** cho tính năng Real-time Notification.
   
2. **Luồng phân quyền và tính năng (Role Authorization):**
   - **Guest (Chưa đăng nhập):** Xem danh sách các bài viết đang ở trạng thái Active.
   - **Lecturer (Role = 2):** Xem tin tức nội bộ.
   - **Admin (Role = 0):** Quản lý tài khoản (CRUD, Search), xem Thống kê Báo cáo (lọc theo ngày StartDate - EndDate).
   - **Staff (Role = 1):** 
     - Quản lý Danh mục (CRUD, chặn xóa nếu danh mục đang được sử dụng).
     - Quản lý Bài viết & Tags (CRUD).
     - Quản lý thông tin hồ sơ cá nhân và Lịch sử bài viết.

3. **Giao diện & UX chuẩn yêu cầu:**
   - Khởi động mặc định vào màn hình **Login**.
   - Các thao tác Create & Update (Thêm/Sửa) hoàn toàn sử dụng **Pop-up Dialog (Modal)** kết hợp AJAX ngầm.
   - Thao tác Delete (Xóa) luôn có hộp thoại xác nhận **Confirmation**.
   
4. **SignalR Real-time Notification:**
   - Bất cứ khi nào Staff đăng bài mới hoặc sửa đổi trạng thái bài viết, một **Toast Notification** (thông báo nổi) sẽ bật lên ở góc màn hình của các Admin/Lecturer đang online.
   - Người dùng có thể click trực tiếp vào Toast để tự động làm mới trang và xem bài viết mới nhất mà không cần F5 thủ công.

---

## 🚀 Hướng dẫn cài đặt và chạy dự án

### 1. Yêu cầu môi trường
- **IDE:** Visual Studio 2022 (hoặc VS Code).
- **Framework:** .NET 8.0 SDK.
- **Database:** Microsoft SQL Server (SSMS).

### 2. Cài đặt Cơ sở dữ liệu (Database)
1. Mở SQL Server Management Studio (SSMS).
2. Chạy file script `FUNewsManagement.sql` (nằm trong thư mục gốc hoặc lấy từ Assignment 1) để tự động khởi tạo Database và dữ liệu mẫu.
3. Mở file cấu hình `appsettings.json` trong project **HoangTranManhDungRazorPages** và điều chỉnh chuỗi kết nối (Connection String) sao cho khớp với tài khoản SA/Windows Authentication của máy tính bạn.
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=FUNewsManagement;Trusted_Connection=True;Encrypt=False;"
   }
   ```

### 3. Build và Chạy dự án
1. Mở Solution `HoangTranManhDung_SE19B_A02.sln` bằng Visual Studio.
2. Thiết lập project **HoangTranManhDungRazorPages** làm *Startup Project*.
3. Nhấn `F5` hoặc `Ctrl + F5` để chạy dự án. Mặc định ứng dụng sẽ tự động mở trang `Login`.

### 4. Tài khoản Demo (Test Accounts)
Bạn có thể sử dụng các tài khoản sau để test luồng phân quyền:

| Role | Email | Password |
| :--- | :--- | :--- |
| **Admin** | `admin@FUNewsManagementSystem.org` | `@@abc123@@` |
| **Staff** | `EmmaWilliam@FUNewsManagement.org` | `@1` |
| **Lecturer** | `IsabellaDavid@FUNewsManagement.org` | `@1` |

---
*Dự án được xây dựng và tối ưu hoàn thiện bởi sinh viên: Hoang Tran Manh Dung - SE19B.*
