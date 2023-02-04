using System;
using UniRx;

namespace Assets.Game.Domain
{
	public class GameAggregate : IGameCommands, IGameEvents, IDisposable
	{
		private readonly Subject<GameEvent> _events = new();

		public IDisposable Subscribe(IObserver<GameEvent> observer) 
			=> _events.Subscribe(observer);

		public void Dispose() 
			=> _events.Dispose();
	}

	public interface IGameCommands
	{
	}

	public interface IGameEvents : IObservable<GameEvent>
	{
	}

	public record GameEvent
	{
	}
}
