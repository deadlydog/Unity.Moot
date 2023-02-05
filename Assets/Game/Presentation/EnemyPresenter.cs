using System;
using Assets.Game.Domain;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class EnemyPresenter : MonoBehaviour, IHaveEnemyId
	{
		public float speed = 100f;
		public Rigidbody2D enemyRigidbody;
		public Vector2 direction = Vector2.left;

		[Inject]
		public EnemyParameters Parameters { private get; set; }

		private void FixedUpdate()
		{
			enemyRigidbody.velocity = direction * speed * Time.fixedDeltaTime;
		}

		public EnemyIdentifier Id => Parameters.EnemyId;
	}
}