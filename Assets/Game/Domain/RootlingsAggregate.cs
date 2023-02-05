using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Assets.Game.Domain
{
	public class RootlingsAggregate : IRootlingsCommands, IRootlingsEvents
	{
		private readonly Subject<RootlingsEvent> _events = new();
		private readonly List<RootlingIdentifier> _rootlings = new();

		public IDisposable Subscribe(IObserver<RootlingsEvent> observer) 
			=> _events.Subscribe(observer);

		public void Register(RootlingIdentifier rootlingId)
		{
			_rootlings.Add(rootlingId);
			_events.OnNext(new RootlingsEvent.Registered(rootlingId, _rootlings.Count));
		}

		public void Steal(EnemyIdentifier enemyId, RootlingIdentifier rootlingId)
		{
			_events.OnNext(new RootlingsEvent.Stolen(enemyId, rootlingId));
		}
	}

	public interface IRootlingsCommands
	{
		void Register(RootlingIdentifier rootlingId);
		void Steal(EnemyIdentifier enemyId, RootlingIdentifier rootlingId);
	}

	public interface IRootlingsEvents : IObservable<RootlingsEvent>
	{
	}

	public abstract record RootlingsEvent
	{
		public record Stolen(EnemyIdentifier EnemyId, RootlingIdentifier RootlingId) : RootlingsEvent;

		public record Registered(RootlingIdentifier RootlingId, int RootlingsCount) : RootlingsEvent;
	}

	public record RootlingIdentifier(int Value)
	{
		private static int _nextValue;

		public static RootlingIdentifier Create()
			=> new(_nextValue++);

		public override string ToString()
			=> $"Rootling {Value}";
	}
}
