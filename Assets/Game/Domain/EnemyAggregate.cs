using System;
using UniRx;

namespace Assets.Game.Domain
{
    public class EnemyAggregate : IEnemyCommands, IEnemyEvents, IDisposable
    {
        private readonly Subject<EnemyEvent> _events = new();
        public void Dispose()
            => _events.Dispose();

        public IDisposable Subscribe(IObserver<EnemyEvent> observer)
            => _events.Subscribe(observer);
    }

    public interface IEnemyCommands
    {
    }

    public interface IEnemyEvents : IObservable<EnemyEvent>
    {
    }

    public record EnemyEvent
    {
    }
}