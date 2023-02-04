using UnityEditor;
using UnityEngine;

namespace Assets.Game.Presentation.Editor
{
	[CustomEditor(typeof(TrunkPresenter))]
	public class TreeGenerator : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			GUILayout.Button("Build");
		}
	}
}
