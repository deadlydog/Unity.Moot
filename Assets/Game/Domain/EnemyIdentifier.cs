namespace Assets.Game.Domain
{
	public record EnemyIdentifier(int Value)
	{
		private static int _nextValue;

		public static EnemyIdentifier Create()
			=> new(_nextValue++);

		public override string ToString()
			=> $"Enemy {Value}";
	}
}