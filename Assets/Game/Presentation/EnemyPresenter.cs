using System;
using Assets.Game.Domain;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class EnemyPresenter : MonoBehaviour, IHaveEnemyId
	{
		public Rigidbody2D enemyRigidbody;
		public Vector2 direction = Vector2.left;

		[Inject]
		public EnemyParameters Parameters { private get; set; }

		private void Start()
		{
			enemyRigidbody.mass = Parameters.EnemyMass;
			transform.localScale = new Vector3(Parameters.EnemyScale, Parameters.EnemyScale, 1.0f);
		}

		private void FixedUpdate()
		{
			enemyRigidbody.velocity = direction * Parameters.EnemySpeed * Time.fixedDeltaTime;
		}

		public EnemyIdentifier Id => Parameters.EnemyId;
	}
}