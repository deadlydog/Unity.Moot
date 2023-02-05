using System;
using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

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
		private EnemyGetHitAudio _enemyGetHitAudio;

		private bool _isDead = false;
		
		[Inject]
		public IEnemyCommands EnemyCommands { private get; set; }
		
		[Inject]
		public EnemyParameters Parameters { private get; set; }

		void Awake()
		{
			_ragdoll = GetComponent<EnemyRagdoll>();
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_enemyDeathAudio = GetComponent<EnemyDeathAudio>();
			_enemyGetHitAudio = GetComponent<EnemyGetHitAudio>();
		}
		
		void OnCollisionEnter2D(Collision2D collision)
		{
			if (collision.otherCollider.IsTouchingLayers(HitLayerMask.value))
			{
				if (collision.contacts[0].relativeVelocity.magnitude > 10)
					TriggerEnemyDeath(collision);
			}
		}

		public void TriggerEnemyDeath(Collision2D collision)
		{
			if (_isDead)
				return;
			_isDead = true;
			
			EnemyCommands.KillEnemy(Parameters.EnemyId);
			
			Observable
					.Timer(TimeSpan.FromSeconds(RagdollDelaySeconds))
					.Subscribe(_ => _ragdoll.RagdollOn());

			var hitDir = transform.position - collision.transform.position;

			Debug.Log(collision.contacts[0].relativeVelocity);

			_rigidbody2d.AddForceAtPosition(
				collision.contacts[0].relativeVelocity * HitForce,
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

			_enemyGetHitAudio.PlayEnemyGetHitSound();
		}
	}
}
