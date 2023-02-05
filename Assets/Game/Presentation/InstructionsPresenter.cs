using System;
using Assets.Game.Domain;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class InstructionsPresenter : MonoBehaviour
	{
		public Canvas InstructionsCanvas;

		[Inject]
		public IEnemyCommands EnemyCommands { private get; set; }

		void Update()
		{
			if (Input.anyKey && InstructionsCanvas.enabled)
			{
				EnemyCommands.StartSpawning();
				InstructionsCanvas.enabled = false;
			}
		}
	}
}
