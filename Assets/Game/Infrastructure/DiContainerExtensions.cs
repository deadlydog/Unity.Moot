using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Infrastructure
{
	public static class DiContainerExtensions
	{
		public static IfNotBoundBinder BindIntegration<TIntegration>(this DiContainer container) where TIntegration : IDisposable
			=> container.Bind<IDisposable>().To<TIntegration>().AsSingle().NonLazy();

		public static ConcreteIdArgConditionCopyNonLazyBinder BindAggregate<TAggregate>(this DiContainer container)
			=> container.BindInterfacesTo<TAggregate>().AsSingle();

		public static void BindPrefabFactory<TParams, TFactory>(this DiContainer container, GameObject prefab,
			Transform parent)
			where TFactory : IFactory<TParams, Unit>
			=> container.BindPrefabFactory<TParams, TFactory, GameObject>(prefab, parent);

		public static void BindPrefabFactory<TParams, TFactory, TPrefab>(this DiContainer container, TPrefab prefab, Transform parent)
			where TFactory : IFactory<TParams, Unit>
		{
			container.BindInstance(prefab).WhenInjectedInto<TFactory>();
			container.BindInstance(parent).WhenInjectedInto<TFactory>();
			container.BindIFactory<TParams, Unit>().FromFactory<TFactory>();
		}

		public static EventBinding<TObservableEvent, TEvent> BindEvent<TObservableEvent, TEvent>(this DiContainer container)
			where TEvent : TObservableEvent
			=> new(container);
	}
}