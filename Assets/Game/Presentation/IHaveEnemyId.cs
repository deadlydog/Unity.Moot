using Assets.Game.Domain;

namespace Assets.Game.Presentation
{
	public interface IHaveEnemyId
	{
		EnemyIdentifier Id { get; }
	}
}