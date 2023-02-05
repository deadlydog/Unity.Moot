using System;
using UniRx;
using UnityEngine;

namespace Assets.Game.Presentation
{
	public class EnemyCollisionPresenter : MonoBehaviour
	{
		public LayerMask HitLayerMask;
		public float HitForce = 1;
		public float RagdollDelaySeconds = 0.3f;
		public float DeathDisappearDelayInSeconds = 1.5f;

		private EnemyRagdoll _ragdoll;
		private Rigidbody2D _rigidbody2d;

		void Awake()
		{
			_ragdoll = GetComponent<EnemyRagdoll>();
			_rigidbody2d = GetComponent<Rigidbody2D>();
		}
		
		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.otherCollider.IsTouchingLayers(HitLayerMask.value))
			{
				TriggerEnemyDeath(collision);
			}
		}

		public void TriggerEnemyDeath(Collision2D collision)
		{
			Observable
					.Timer(TimeSpan.FromSeconds(RagdollDelaySeconds))
					.Subscribe(_ => _ragdoll.RagdollOn());

			var hitDir = transform.position - collision.transform.position;

			_rigidbody2d.AddForceAtPosition(
				hitDir.normalized * HitForce,
				collision.GetContact(0).point
			);
			
			Observable
				.Timer(TimeSpan.FromSeconds(DeathDisappearDelayInSeconds))
				.Subscribe(_ => Destroy(gameObject));

			// Play death scream sound.

		}
	}
}
