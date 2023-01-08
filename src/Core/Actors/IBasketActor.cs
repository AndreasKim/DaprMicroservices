using Dapr.Actors;

namespace Actors;

public interface IBasketActor : IActor
{
    Task<bool> SetDataAsync(Basket data);
    Task<Basket> GetDataAsync();
    Task RegisterReminder(TimeSpan firstInvocation);
    Task UnregisterReminder();
    Task RegisterTimer();
    Task UnregisterTimer();
}

public class Basket
{
    public List<int> ProductIds { get; set; } = new List<int>();
}
