using UnityEngine;

namespace Assets.Game.Domain
{
    public record EnemyConfig(Vector2 Position, float EnemyMass, float EnemyRagdollLimbMass, float EnemySpeed, float EnemyScale);

    public record EnemyParameters(EnemyIdentifier EnemyId, Vector2 Position, float EnemyMass, float EnemyRagdollLimbMass, float EnemySpeed, float EnemyScale);
}