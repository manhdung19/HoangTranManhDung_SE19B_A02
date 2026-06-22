# Kỹ năng Bắt buộc: Đọc kỹ Interface/Service trước khi viết Code-behind (PageModel)

## 1. Vấn đề (Bối cảnh)
Trong các dự án có cấu trúc nhiều tầng (N-Tier Architecture), đặc biệt là khi tiếp quản source code cũ (như chuyển từ MVC của Assignment 1 sang Razor Pages của Assignment 2), các lập trình viên trước đó thường có thiết kế chữ ký hàm (method signatures) không đồng nhất.
- Ví dụ: Hàm xóa Account thì yêu cầu tham số là cả một đối tượng `SystemAccount` (`DeleteSystemAccount(SystemAccount account)`).
- Trong khi đó, hàm xóa Category hay NewsArticle lại chỉ yêu cầu tham số là một ID (`DeleteCategory(short id)` hoặc `DeleteNewsArticle(string id)`).

## 2. Hậu quả
Nếu AI (hoặc lập trình viên) tự "đoán" hoặc "mặc định" các hàm sẽ có chung một chuẩn quy tắc thống nhất (Ví dụ: mặc định hàm Delete nào cũng dùng chung ID), thì khi sinh code cho các file `.cshtml.cs` sẽ ngay lập tức gây ra các lỗi Compile-time làm gián đoạn luồng làm việc của User:
- `CS1503: Argument 1: cannot convert from 'short' to 'Entity'`
- `CS7036: There is no argument given that corresponds to the required parameter...`

## 3. Quy tắc hành động bắt buộc (Skill Rule)
Để chấm dứt hoàn toàn tình trạng này, từ nay về sau, trước khi cung cấp mã nguồn (code) cho bất kỳ file `.cshtml.cs` (PageModel) nào có gọi đến tầng Service/BLL, AI **BẮT BUỘC** phải tuân thủ nghiêm ngặt quy trình sau:

1. **Tra cứu Interface/Implementation**: Sử dụng công cụ `grep_search` hoặc `view_file` để tìm đến file Interface tương ứng (VD: `ISystemAccountService.cs`, `ICategoryService.cs`, `INewsArticleService.cs`).
2. **Xác nhận Chữ ký hàm (Method Signatures)**: Quét và đọc chính xác kiểu dữ liệu trả về (Return Type) và danh sách tham số đầu vào (Parameters) của hàm mục tiêu chuẩn bị sử dụng.
3. **Phân tích tham số**: Chú ý kỹ xem hàm đó yêu cầu truyền vào Đối tượng (Entity) hay ID (Primitive Type), có yêu cầu các tham số phụ như `searchString`, `tags[]` không.
4. **Sinh mã an toàn**: Chỉ sau khi nắm chắc 100% hình thù của hàm ở bước 2 và 3, AI mới được phép sinh code cho các logic Handler (`OnGet`, `OnPost...`). **Tuyệt đối không được "đoán" tham số theo thói quen**.
