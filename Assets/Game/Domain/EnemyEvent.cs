namespace Assets.Game.Domain
{
    public abstract record EnemyEvent
    {
        public record EnemySpawned : EnemyEvent
        {
            public EnemySpawned(EnemyIdentifier enemyId, EnemyConfig enemyConfig)
            {
                EnemyId = enemyId;
                EnemyConfig = enemyConfig;
            }

            public EnemyIdentifier EnemyId { get; }
            public EnemyConfig EnemyConfig { get; }
        }

        public record EnemyKilled : EnemyEvent
        {
            public EnemyKilled(EnemyIdentifier enemyId)
            {
                EnemyId = enemyId;
            }

            public EnemyIdentifier EnemyId { get; }
        }

        public record AllEnemiesKilled : EnemyEvent
        {
        }
    }
}