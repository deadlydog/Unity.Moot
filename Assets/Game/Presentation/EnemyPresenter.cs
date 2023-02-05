using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class EnemyPresenter : MonoBehaviour, IHaveEnemyId
	{
		public Rigidbody2D enemyRigidbody;
		public Vector2 direction = Vector2.left;
		public int RightMoveLayer;

		[Inject]
		public EnemyParameters Parameters { private get; set; }

		[Inject]
		public IRootlingsEvents RootlingsEvents { private get; set; }

		void Start()
		{
			enemyRigidbody.mass = Parameters.EnemyMass;
			transform.localScale = new Vector3(Parameters.EnemyScale, Parameters.EnemyScale, 1.0f);
			
			RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.Stolen>()
				.Where(stolen => stolen.EnemyId == Parameters.EnemyId)
				.Subscribe(_ =>
				{
					direction = Vector2.right;
					transform.localScale = new Vector3(
						transform.localScale.x * -1, 
						transform.localScale.y,
						transform.localScale.z
					);

					transform.gameObject.layer = RightMoveLayer;
				})
				.AddTo(this);
		}

		private void FixedUpdate()
		{
			enemyRigidbody.velocity = direction * Parameters.EnemySpeed * Time.fixedDeltaTime;
		}

		public EnemyIdentifier Id => Parameters.EnemyId;
	}
}