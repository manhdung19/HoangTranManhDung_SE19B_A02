# Lộ trình hoàn thiện PRN222 Assignment 02
**Mục tiêu:** Chuyển đổi dự án từ kiến trúc ASP.NET Core MVC sang **ASP.NET Core Razor Pages** và tích hợp **SignalR** (Real-time).

---

## 🟢 Giai đoạn 1: Khởi tạo và Cấu hình Project (Setup & Config)
1. **Tạo Project mới:** 
   - Xóa bỏ (hoặc tách riêng) project `HoangTranManhDungMVC` cũ để không bị nhầm lẫn.
   - Tạo Project mới tên là **`HoangTranManhDungRazorPages`** với template *ASP.NET Core Web App (Razor Pages)*.
2. **Kế thừa kiến trúc:** 
   - Thêm Reference Project `FUNewsManagement.BLL` vào Project Razor Pages mới.
   - Tái sử dụng 100% tầng DAO, Model, Repository và Service.
3. **Cấu hình `Program.cs`:**
   - Copy chuỗi kết nối Database `appsettings.json`.
   - Đăng ký Dependency Injection (DI) cho các Services (`ISystemAccountService`, `ICategoryService`, v.v...).
   - Khai báo và cấu hình **Session** để quản lý trạng thái đăng nhập.

---

## 🟡 Giai đoạn 2: Xác thực & Điều hướng (Auth & Navigation)
1. **Chuyển đổi màn hình Đăng nhập:**
   - Chuyển logic từ `AuthController.cs` cũ sang file `Pages/Login.cshtml` và `Pages/Login.cshtml.cs`.
   - Thiết lập trang mặc định khi vừa mở app lên là `/Login`.
2. **Tái tạo Navigation Bar (`_Layout.cshtml`):**
   - Bưng thanh menu tự động nhận diện Role từ project cũ sang.
   - Chỉnh sửa đường dẫn các thẻ `<a>` từ `asp-controller` sang `asp-page`.
3. **Đăng xuất (Logout):**
   - Viết handler xử lý clear Session và trả về trang Login.

---

## 🟠 Giai đoạn 3: Tính năng xem tin tức (Public & Lecturer)
1. **Public News (Khách vãng lai):**
   - Dùng trang `Pages/Index.cshtml` làm nơi hiển thị tin tức Public.
   - Lọc các tin tức có `NewsStatus = true`.
2. **Lecturer News:**
   - Tạo thư mục `Pages/Lecturer`.
   - Hiển thị danh sách tin tức (tương tự Public) nhưng yêu cầu chặn quyền chỉ cho Role = 2 vào xem.

---

## 🟢 Giai đoạn 4: Chuyển đổi CRUD & Popups (Admin & Staff)
*Đã hoàn thành 100% việc chuyển đổi các lời gọi AJAX từ MVC sang dạng Page Handlers.*

1. **Quản lý Tài khoản & Report (Admin): [DONE]**
   - Chuyển giao diện Account sang Razor Pages.
   - Đổi các AJAX url gọi `Create/Edit` sang dạng `?handler=Create`.
   - Chuyển tính năng lọc Báo cáo (Report) bằng Date.
2. **Quản lý Danh mục (Staff): [DONE]**
   - Chuyển giao diện Category.
   - Giữ nguyên logic chặn xóa Category nếu đang có News tham chiếu tới.
3. **Hồ sơ cá nhân & Lịch sử bài viết (Staff): [DONE]**
   - Chuyển trang Profile và News History.
4. **Quản lý Bài viết & Tags (Staff): [DONE]**
   - Chuyển giao diện xử lý Bài viết. Đảm bảo logic Create/Edit lưu được mảng nhiều Tags (`List<int> tagIds`) vào bảng trung gian.

---

## 🟢 Giai đoạn 5: Tích hợp SignalR (Thời gian thực)
*Điểm khác biệt cốt lõi nhất của Assignment 2.*

1. **Setup SignalR Server: [DONE]**
   - Đã tạo class `Hubs/NewsHub.cs`.
   - Đã đăng ký `app.MapHub<NewsHub>("/newshub")` vào `Program.cs`.
2. **Bắn thông báo (Trigger): [DONE]**
   - Đã nhúng `IHubContext<NewsHub>` vào `NewsArticle/Index.cshtml.cs`.
   - Các hàm `OnPostCreate` và `OnPostEdit` đã đổi thành `Async` và gọi lệnh `_hubContext.Clients.All.SendAsync("ReceiveNewsNotification", ...)` khi lưu thành công.
3. **Nhận thông báo & Hiển thị (Client-side): [DONE]**
   - Thêm thư viện `signalr.min.js` vào `_Layout.cshtml`.
   - Thêm mã JavaScript tự động kết nối `/newshub`.
   - Thêm một khối **Bootstrap Toast** đẹp mắt màu xanh lá cây ở góc dưới bên phải màn hình để hiển thị thông báo ngay lập tức mà không cần F5.

---

## 🏁 Giai đoạn 6: Testing & Hoàn thiện
- Rà soát lại tất cả Validation (Required, Format).
- Chạy thử luồng: Staff đăng bài -> Chờ vài giây -> Màn hình của Lecturer/Admin tự động nhảy thông báo (Pop-up Toast) mà không cần F5 tải lại trang.
- Đóng gói (Zip) và chuẩn bị file SQL để nộp bài.
