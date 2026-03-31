using BangBoTrafficApi.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. เพิ่ม Services เข้าไปในระบบ (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ช่วยสร้างหน้าทดสอบ API
builder.Services.AddSingleton<IPlcService, PlcService>();

// 2. ตั้งค่า CORS (สำคัญมากสำหรับหน้าเว็บ JS)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// 3. ใช้งาน Middleware ต่างๆ
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();