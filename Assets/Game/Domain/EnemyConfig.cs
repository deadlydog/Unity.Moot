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
}