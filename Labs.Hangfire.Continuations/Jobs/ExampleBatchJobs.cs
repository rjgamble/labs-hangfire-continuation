using Hangfire;

namespace Labs.Hangfire.Continuations.Jobs;

public class ExampleBatchJobs
{
    private static bool succeed = false;

    public void Apple()
    {
        Console.WriteLine("Processing Apple job");
    }

    [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public void Banana()
    {
        throw new InvalidOperationException("Cant do bananas today");
    }

    public void Kiwi()
    {
        Console.WriteLine("Processing Kiwi job");
    }
}
