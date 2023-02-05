using UnityEngine;

namespace Assets.Game.Domain
{
    public interface IEnemyCommands
    {
		public record EnemyParameters(float EnemyMass, float EnemyRagdollLimbMass, float EnemySpeed, float EnemyScale);

		void SpawnEnemy(Vector2 position, EnemyParameters enemyParameters);
        void KillEnemy(EnemyIdentifier enemyId);
    }
}