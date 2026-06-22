using Microsoft.AspNetCore.SignalR;

namespace HoangTranManhDungRazorPages.Hubs
{
    public class NewsHub : Hub
    {
        // Hub này dùng để đẩy thông báo bài viết mới/cập nhật xuống Client
        // Không cần định nghĩa method ở đây nếu server chủ động push data
    }
}
