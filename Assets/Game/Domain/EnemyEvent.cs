namespace Assets.Game.Domain
{
	public abstract record EnemyEvent
	{
		public record EnemySpawned(EnemyIdentifier EnemyId, EnemyConfig EnemyConfig) : EnemyEvent;

		public record EnemyKilled(EnemyIdentifier EnemyId) : EnemyEvent;

		public record AllEnemiesKilled : EnemyEvent;

		public record SpawningStarted : EnemyEvent;
	}
}