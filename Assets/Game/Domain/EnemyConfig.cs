using UnityEngine;

namespace Assets.Game.Domain
{
    public record EnemyConfig
    {
        public Vector2 Position { get; }

        public EnemyConfig(Vector2 position)
        {
            Position = position;
        }
    }

    public record EnemyParameters
    {
        public EnemyParameters(EnemyIdentifier enemyId, Vector2 position)
        {
            EnemyId = enemyId;
            Position = position;
        }

        public Vector2 Position { get; }
        public EnemyIdentifier EnemyId { get; }
    }
}