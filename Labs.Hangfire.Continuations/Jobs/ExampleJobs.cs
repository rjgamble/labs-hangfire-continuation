public class ExampleJobs
{
    public Task JobApple()
    {
        Console.WriteLine("Begin Job: apple");
        Thread.Sleep(3000);
        Console.WriteLine("End Job: apple");
        return Task.CompletedTask;
    }

    public Task JobBanana()
    {
        Console.WriteLine("Begin Job: banana");
        Thread.Sleep(3000);
        Console.WriteLine("End Job: banana");
        return Task.CompletedTask;
    }

    public Task JobKiwi()
    {
        Console.WriteLine("Begin Job: kiwi");
        Thread.Sleep(3000);
        Console.WriteLine("End Job: kiwi");
        return Task.CompletedTask;
    }
}