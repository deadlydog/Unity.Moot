using System;
using Zenject;

namespace Assets.Game.Infrastructure
{
	public class EventBinding<TObservableEvent, TEvent> where TEvent : TObservableEvent
	{
		private readonly DiContainer _container;

		public EventBinding(DiContainer container)
		{
			_container = container;
		}
		
		public void ToCommand<TCommands, TFailure>(Action<TEvent, TCommands> binding)
		{
			_container
				.BindInstance(binding)
				.WhenInjectedInto<EventCommandBinding<TObservableEvent, TEvent, TCommands, TFailure>>();

			_container
				.Bind<IDisposable>()
				.To<EventCommandBinding<TObservableEvent, TEvent, TCommands, TFailure>>()
				.AsSingle()
				.NonLazy();
		}

		public void ToAction(Action<TEvent> action)
		{
			_container
				.BindInstance(action)
				.WhenInjectedInto<EventActionBinding<TObservableEvent, TEvent>>();

			_container
				.Bind<IDisposable>()
				.To<EventActionBinding<TObservableEvent, TEvent>>()
				.AsSingle()
				.NonLazy();
		}

		public void ToFactory<TParams>(Func<TEvent, TParams> map)
		{
			_container
				.BindInstance(map)
				.WhenInjectedInto<EventFactoryBinding<TObservableEvent, TEvent, TParams>>();

			_container
				.Bind<IDisposable>()
				.To<EventFactoryBinding<TObservableEvent, TEvent, TParams>>()
				.AsSingle()
				.NonLazy();
		}
	}
}