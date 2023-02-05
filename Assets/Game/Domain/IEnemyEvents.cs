using System;

namespace Assets.Game.Domain
{
    public interface IEnemyEvents : IObservable<EnemyEvent>
    {
    }
}