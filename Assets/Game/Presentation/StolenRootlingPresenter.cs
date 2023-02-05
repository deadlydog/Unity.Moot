using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class StolenRootlingPresenter : MonoBehaviour
	{
		private SpriteRenderer _spriteRenderer;

		[Inject]
		public EnemyParameters Parameters { private get; set; }

		[Inject]
		public IRootlingsEvents RootlingsEvents { private get; set; }

		void Awake()
		{
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void Start()
		{
			SetStolenRootlingVisibility(false);

			RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.Stolen>()
				.Where(stolen => stolen.EnemyId == Parameters.EnemyId)
				.Subscribe(_ => SetStolenRootlingVisibility(true))
				.AddTo(this);
		}

		private void SetStolenRootlingVisibility(bool visible) 
			=> _spriteRenderer.enabled = visible;
	}
}
