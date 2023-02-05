using Assets.Game.Domain;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class RootlingPresenter : MonoBehaviour
	{
		private RootlingIdentifier _id;

		[Inject]
		public IRootlingsCommands RootlingsCommands { private get; set; }

		void Awake()
		{
			_id = RootlingIdentifier.Create();
		}

		// Start is called before the first frame update
		void Start()
		{
			RootlingsCommands.Register(_id);
		}
		
		void OnTriggerEnter2D(Collider2D otherCollider)
		{
			Debug.Log("Rootling collision");

			var enemy = otherCollider.GetComponent<IHaveEnemyId>();

			if (enemy != null)
			{
				Debug.Log($"{enemy.Id} stealing {_id}");
				RootlingsCommands.Steal(enemy.Id, _id);
			}
		}
	}

	public interface IHaveEnemyId
	{
		EnemyIdentifier Id { get; }
	}
}
