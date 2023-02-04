using System.Collections.Generic;
using UnityEditor;
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
				BuildTree((target as TrunkPresenter).gameObject);
			}
		}

		private void BuildTree(GameObject trunk)
		{
			Debug.Log($"Build for {trunk}");

			var rigidbody = trunk.GetOrAddComponent<Rigidbody2D>();
			rigidbody.bodyType = RigidbodyType2D.Static;
			rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

			var spriteSkin = trunk.GetComponent<SpriteSkin>();

			foreach (var (parentBone, bone) in spriteSkin.boneTransforms.Pairwise())
			{
				BuildBranchBone(parentBone.gameObject, bone.gameObject);
			}
		}

		private void BuildBranchBone(GameObject parentBone, GameObject branchBone)
		{
			var rigidbody = branchBone.GetOrAddComponent<Rigidbody2D>();
			rigidbody.bodyType = RigidbodyType2D.Dynamic;
			rigidbody.angularDrag = 100;
			rigidbody.mass = 0.5f;
			rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

			var hinge = branchBone.GetOrAddComponent<HingeJoint2D>();
			hinge.connectedBody = parentBone.GetComponent<Rigidbody2D>();

			var springJoint = branchBone.GetOrAddComponent<AngularSpringJoint>();
			springJoint.TorqueCoefficient = 1;

			var collider = branchBone.GetOrAddComponent<CapsuleCollider2D>();
			collider.direction = CapsuleDirection2D.Horizontal;
			collider.size = new Vector2(1, 0.4f);
			collider.offset = new Vector2(0.4f, 0);
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
