using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Dashboard.Dark;
using Labs.Hangfire.Continuations.Jobs;

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
    options.UseMemoryStorage();
    options.UseBatches();
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
    var appleId = BackgroundJob.Enqueue<ExampleJobs>(job => job.JobApple());
    Console.WriteLine("Added Job: apple with ID {0}", appleId.ToString());

    var bananaId = BackgroundJob.ContinueJobWith<ExampleJobs>(appleId, job => job.JobBanana());
    Console.WriteLine("Added Job: banana with ID {0}", bananaId.ToString());

    var kiwiId = BackgroundJob.ContinueJobWith<ExampleJobs>(bananaId, job => job.JobKiwi());
    Console.WriteLine("Added Job: kiwi with ID {0}", kiwiId.ToString());
})
.WithName("Enqueue");

app.MapPost("/enqueue-pro", (IBackgroundJobClient backgroundJobClient) =>
{
    var batchId = BatchJob.StartNew(q =>
    {
        q.Enqueue<ExampleBatchJobs>(j => j.Apple());
        q.Enqueue<ExampleBatchJobs>(j => j.Banana());
    });

    backgroundJobClient.ContinueBatchWith<ExampleBatchJobs>(batchId,
                                                            j => j.Kiwi(),
                                                            options: Hangfire.Batches.BatchContinuationOptions.OnAnyFinishedState);
})
.WithName("Enqueue Pro");

app.Run();
