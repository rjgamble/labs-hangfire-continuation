public class ExampleJobs
{
    public async Task JobApple()
    {
        Console.WriteLine("Begin Job: apple");
        await Task.Delay(3000);
        Console.WriteLine("End Job: apple");
    }

    public async Task JobBanana()
    {
        Console.WriteLine("Begin Job: banana");
        await Task.Delay(3000);
        Console.WriteLine("End Job: banana");
    }

    public async Task JobKiwi()
    {
        Console.WriteLine("Begin Job: kiwi");
        await Task.Delay(3000);
        Console.WriteLine("End Job: kiwi");
    }
}