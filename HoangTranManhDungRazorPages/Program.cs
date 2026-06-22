using FUNewsManagement.BLL.Services.Implementations;
using FUNewsManagement.BLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages(options =>
{
    // Đặt trang Login làm trang mặc định khi vừa mở Web
    options.Conventions.AddPageRoute("/Login", "");
});

// 1. Đăng ký Dependency Injection (DI) cho các Services
builder.Services.AddScoped<ISystemAccountService, SystemAccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<INewsArticleService, NewsArticleService>();
builder.Services.AddScoped<ITagService, TagService>();

// 2. Cấu hình Session để giữ trạng thái đăng nhập
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. Đăng ký SignalR (Chuẩn bị cho tính năng Real-time)
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Kích hoạt Session (Phải nằm giữa UseRouting và UseAuthorization)
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

// Đăng ký endpoint cho SignalR Hub
app.MapHub<HoangTranManhDungRazorPages.Hubs.NewsHub>("/newshub");

app.Run();
