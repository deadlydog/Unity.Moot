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
		public float ChanceOfDeathScream = 0.25f;

		private EnemyRagdoll _ragdoll;
		private Rigidbody2D _rigidbody2d;
		private EnemyDeathAudio _enemyDeathAudio;

		private bool _isDead = false;

		void Awake()
		{
			_ragdoll = GetComponent<EnemyRagdoll>();
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_enemyDeathAudio = GetComponent<EnemyDeathAudio>();
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
			if (_isDead)
				return;
			_isDead = true;
			
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

			// Too many screams are annoying.
			if (UnityEngine.Random.Range(0f, 1f) < ChanceOfDeathScream)
			{
				_enemyDeathAudio.PlayEnemyDeathScream();
			}
		}
	}
}
