using System;
using Assets.Game.Domain;
using UniRx;
using Zenject;

namespace Assets.Game.Infrastructure.Integrations
{
    public class CreateNewEnemyOnEnemySpawned : IDisposable
    {
        private readonly IDisposable _disposable;

        public CreateNewEnemyOnEnemySpawned(IEnemyEvents enemyEvents, IFactory<EnemyParameters, Unit> enemyFactory)
        {
            _disposable = enemyEvents
                .OfType<EnemyEvent, EnemyEvent.EnemySpawned>()
                .Subscribe(enemySpawned =>
                    enemyFactory.Create(new EnemyParameters(enemySpawned.EnemyId, enemySpawned.EnemyConfig.Position, enemySpawned.EnemyConfig.EnemyMass, enemySpawned.EnemyConfig.EnemyRagdollLimbMass, enemySpawned.EnemyConfig.EnemySpeed, enemySpawned.EnemyConfig.EnemyScale))
				);
        }
        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}