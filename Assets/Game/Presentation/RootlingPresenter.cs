using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class RootlingPresenter : MonoBehaviour
	{
		private RootlingIdentifier _id;

		[Inject]
		public IRootlingsCommands RootlingsCommands { private get; set; }

		[Inject]
		public IRootlingsEvents RootlingsEvents { private get; set; }

		void Awake()
		{
			_id = RootlingIdentifier.Create();
		}

		// Start is called before the first frame update
		void Start()
		{
			Observable
				.NextFrame()
				.Subscribe(_ => RootlingsCommands.Register(_id))
				.AddTo(this);

			RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.Stolen>()
				.Where(stolen => stolen.RootlingId == _id)
				.Subscribe(_ => Destroy(gameObject))
				.AddTo(this);
		}
		
		void OnTriggerEnter2D(Collider2D otherCollider)
		{
			var enemy = otherCollider.GetComponent<IHaveEnemyId>();

			if (enemy != null)
			{
				Debug.Log($"{enemy.Id} stealing {_id}");
				RootlingsCommands.Steal(enemy.Id, _id);
			}
		}
	}
}
