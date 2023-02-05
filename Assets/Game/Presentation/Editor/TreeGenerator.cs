using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.U2D.Animation;

namespace Assets.Game.Presentation.Editor
{
	[CustomEditor(typeof(TrunkPresenter))]
	public class TreeGenerator : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button("Build"))
			{
				Debug.Log($"Target = {target}, serializedObject = {serializedObject}");

				var trunkPresenter = (TrunkPresenter)target;
				var trunk = trunkPresenter.gameObject;

				BuildTree(trunk, trunkPresenter.InitialParameters);

				EditorUtility.SetDirty(target);
				EditorSceneManager.MarkSceneDirty(trunk.scene);
			}
		}

		private void BuildTree(GameObject trunk, BranchParameters branchParams)
		{
			Debug.Log($"Build for {trunk}");

			var rigidbody = trunk.GetOrAddComponent<Rigidbody2D>();
			rigidbody.bodyType = RigidbodyType2D.Kinematic;
			rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

			var spriteSkin = trunk.GetComponent<SpriteSkin>();

			foreach (var (parentBone, bone) in spriteSkin.boneTransforms.Pairwise())
			{
				BuildBranchBone(parentBone.gameObject, bone.gameObject, branchParams);

				branchParams = branchParams.Propagate();
			}
		}

		private void BuildBranchBone(GameObject parentBone, GameObject branchBone, BranchParameters branchParams)
		{
			var rigidbody = branchBone.GetOrAddComponent<Rigidbody2D>();
			rigidbody.bodyType = RigidbodyType2D.Dynamic;
			rigidbody.angularDrag = branchParams.AngularDrag;
			rigidbody.mass = branchParams.Mass;
			rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

			var hinge = branchBone.GetOrAddComponent<HingeJoint2D>();
			hinge.connectedBody = parentBone.GetComponent<Rigidbody2D>();
			hinge.autoConfigureConnectedAnchor = false;

			var springJoint = branchBone.GetOrAddComponent<AngularSpringJoint>();
			springJoint.TorqueCoefficient = branchParams.TorqueCoefficient;
			springJoint.DifferentialCoeff = branchParams.DifferentialCoefficient;
			springJoint.IntegralCoeff = branchParams.IntegralCoefficient;

			var collider = branchBone.GetOrAddComponent<CapsuleCollider2D>();
			collider.direction = CapsuleDirection2D.Horizontal;
			collider.size = new Vector2(1, 0.4f);
			collider.offset = new Vector2(0.4f, 0);

			foreach (Transform childTransform in branchBone.transform)
			{
				var spriteSkin = childTransform.GetComponent<SpriteSkin>();
				if (spriteSkin != null)
					BuildTree(childTransform.gameObject, branchParams.ScaleMass(childTransform.localScale.x));
			}
		}

		
	}

	public static class HandyExtensions
	{
		public static IEnumerable<(T, T)> Pairwise<T>(this IEnumerable<T> source)
		{
			using var enumerator = source.GetEnumerator();

			if (!enumerator.MoveNext())
				yield break;

			var previous = enumerator.Current;

			while (enumerator.MoveNext())
			{
				var current = enumerator.Current;
				yield return (previous, current);
				previous = current;
			}
		}

		public static T GetOrAddComponent<T>(this GameObject gameObject)
			where T : Component
		{
			var component = gameObject.GetComponent<T>();

			return component == null
				? gameObject.AddComponent<T>()
				: component;
		}
	}
}
