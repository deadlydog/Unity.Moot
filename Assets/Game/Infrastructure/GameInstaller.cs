using Assets.Game.Domain;
using Assets.Game.Infrastructure.Integrations;
using UnityEngine;
using Zenject;

namespace Assets.Game.Infrastructure
{
	public class GameInstaller : MonoInstaller
	{
		public Transform EnemiesContainer;
		public GameObject EnemyPrefab;
		public override void InstallBindings()
		{
			Container.BindInterfacesTo<EnemyAggregate>().AsSingle().NonLazy();
			Container.BindPrefabFactory<EnemyParameters, EnemyFactory>(EnemyPrefab, EnemiesContainer);
			Container.BindIntegration<CreateNewEnemyOnEnemySpawned>();
		}
	}
}
