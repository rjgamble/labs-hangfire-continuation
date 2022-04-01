using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Dashboard.Dark;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ExampleJobs>(new ExampleJobs());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire((serviceProvider, options) => 
{
    options.UseDarkDashboard();

    var config = serviceProvider.GetRequiredService<IConfiguration>();

    options.UseMemoryStorage();
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapPost("/enqueue", () => 
{
    BackgroundJob.Schedule<ExampleJobs>(job => job.JobApple(), TimeSpan.Zero);

    var appleId = BackgroundJob.Enqueue<ExampleJobs>(job => job.JobApple());
    Console.WriteLine("Added Job: apple with ID {@id}", appleId);

    var bananaId = BackgroundJob.ContinueJobWith<ExampleJobs>(appleId, job => job.JobBanana());
    Console.WriteLine("Added Job: banana with ID {@id}", bananaId);

    var kiwiId = BackgroundJob.ContinueJobWith<ExampleJobs>(bananaId, job => job.JobKiwi());
    Console.WriteLine("Added Job: kiwi with ID {@id}", kiwiId);
})
.WithName("Enqueue");

app.Run();
