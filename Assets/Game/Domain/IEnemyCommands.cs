using UnityEngine;

namespace Assets.Game.Domain
{
    public interface IEnemyCommands
    {
        void SpawnEnemy(Vector2 position);
        void KillEnemy(EnemyIdentifier enemyId);
    }
}