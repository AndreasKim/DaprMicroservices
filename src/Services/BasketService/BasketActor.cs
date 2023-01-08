using Actors;
using Dapr.Actors.Runtime;

namespace BasketService;

public class BasketActor : Actor, IRemindable, IBasketActor
{
    private const string StateName = "basket_data";
    private readonly ILogger<BasketActor> _logger;

    public BasketActor(ActorHost host, ILogger<BasketActor> logger)
        : base(host)
    {
        _logger = logger;
    }

    protected override Task OnActivateAsync()
    {
        _logger.LogInformation("Activating actor id: {Id}", this.Id);
        return Task.CompletedTask;
    }

    protected override Task OnDeactivateAsync()
    {
        _logger.LogInformation("Deactivating actor id: {Id}", this.Id);
        return Task.CompletedTask;
    }

    public async Task<bool> SetDataAsync(Basket data)
    {
        await this.StateManager.SetStateAsync(
            StateName,
            data);

        return true;
    }

    public Task<Basket> GetDataAsync()
    {
        return this.StateManager.GetStateAsync<Basket>(StateName);
    }

    public async Task RegisterReminder(TimeSpan firstInvocation)
    {
        await this.RegisterReminderAsync(
            "MyReminder",              // The name of the reminder
            null,                      // User state passed to IRemindable.ReceiveReminderAsync()
            firstInvocation,   // Time to delay before invoking the reminder for the first time
            TimeSpan.FromSeconds(5));  // Time interval between reminder invocations after the first invocation
    }

    public Task UnregisterReminder()
    {
        _logger.LogInformation("Unregistering MyReminder...");
        return this.UnregisterReminderAsync("MyReminder");
    }

    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        _logger.LogInformation("ReceiveReminderAsync is called!");
        return Task.CompletedTask;
    }

    public Task RegisterTimer()
    {
        return this.RegisterTimerAsync(
            "MyTimer",                  // The name of the timer
            nameof(this.OnTimerCallBack),       // Timer callback
            null,                       // User state passed to OnTimerCallback()
            TimeSpan.FromSeconds(5),    // Time to delay before the async callback is first invoked
            TimeSpan.FromSeconds(5));   // Time interval between invocations of the async callback
    }

    public Task UnregisterTimer()
    {
        Console.WriteLine("Unregistering MyTimer...");
        return this.UnregisterTimerAsync("MyTimer");
    }

    private Task OnTimerCallBack(byte[] data)
    {
        Console.WriteLine("OnTimerCallBack is called!");
        return Task.CompletedTask;
    }
}
