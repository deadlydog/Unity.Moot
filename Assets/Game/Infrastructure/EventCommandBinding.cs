using System;
using UniRx;

namespace Assets.Game.Infrastructure
{
	public class EventCommandBinding<TObservableEvent, TEvent, TCommands, TFailure> : IDisposable
	{
		private readonly IDisposable _disposable;

		public EventCommandBinding(
			IObservable<TObservableEvent> events,
			TCommands commands,
			Action<TEvent, TCommands> binding
		) => _disposable = events
			.OfType<TObservableEvent, TEvent>()
			.Subscribe(@event => binding(@event, commands));

		public void Dispose()
			=> _disposable.Dispose();
	}
}