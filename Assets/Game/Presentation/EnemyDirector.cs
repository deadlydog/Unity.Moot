using System;
using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class EnemyDirector : MonoBehaviour
	{
		public float SpawnDelaySeconds = 2.0f;

		[Inject]
		public IEnemyCommands EnemyCommands { private get; set; }

		[Inject]
		public IEnemyEvents EnemyEvents { private get; set; }

		private void Start()
		{
			var delayTimeSpan = TimeSpan.FromSeconds(SpawnDelaySeconds);

			Observable.Timer(delayTimeSpan, delayTimeSpan)
				.Subscribe(_ => SpawnAnEnemy())
				.AddTo(this);
		}

		public void SpawnAnEnemy()
			=> EnemyCommands.SpawnEnemy(transform.position);
	}

}