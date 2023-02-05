using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Zenject;
using UnityEngine;

namespace Assets.Game.Domain
{
	public class EnemyAggregate : IEnemyCommands, IEnemyEvents, IDisposable
	{
		private readonly Subject<EnemyEvent> _events = new();
		private readonly ISet<EnemyIdentifier> _enemies = new HashSet<EnemyIdentifier>();
		public void Dispose()
			=> _events.Dispose();

		public IDisposable Subscribe(IObserver<EnemyEvent> observer)
			=> _events.Subscribe(observer);

		public void SpawnEnemy(Vector2 position, IEnemyCommands.EnemyParameters enemyParameters)
		{
			var enemyId = EnemyIdentifier.Create();
			_enemies.Add(enemyId);
			_events.OnNext(new EnemyEvent.EnemySpawned(enemyId, new EnemyConfig(position, enemyParameters.EnemyMass, enemyParameters.EnemyRagdollLimbMass, enemyParameters.EnemySpeed, enemyParameters.EnemyScale)));
		}

		public void KillEnemy(EnemyIdentifier enemyId)
		{
			// Debug.Log($"Kill enemy: {enemyId}");
			if (!_enemies.Remove(enemyId))
				throw new ArgumentException($"{enemyId} doesn't exist");

			_events.OnNext(new EnemyEvent.EnemyKilled(enemyId));

			if (_enemies.Any()) return;

			_events.OnNext(new EnemyEvent.AllEnemiesKilled());
		}

		public void StartSpawning()
		{
			_events.OnNext(new EnemyEvent.SpawningStarted());
		}
	}
}