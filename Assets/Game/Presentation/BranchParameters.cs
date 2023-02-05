using System;

namespace Assets.Game.Presentation
{
	[Serializable]
	public record BranchParameters
	{
		public float TorqueCoefficient;
		public float Mass;
		public float AngularDrag;
		public float MassPropagation;
		public float AngularDragPropagation;

		public BranchParameters Propagate()
			=> this with
			{
				Mass = Mass * MassPropagation,
				AngularDrag = AngularDrag * AngularDragPropagation
			};

		public BranchParameters ScaleMass(float massScale)
			=> this with { Mass = Mass * massScale };
	}
}