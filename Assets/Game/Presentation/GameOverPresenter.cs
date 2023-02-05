using System;
using Assets.Game.Domain;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Game.Presentation
{
	public class GameOverPresenter : MonoBehaviour
	{
		private TextMeshProUGUI _text;

		[Inject]
		public IRootlingsEvents RootlingsEvents { private get; set; }

		void Awake()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		void Start()
		{
			SetGameOverVisibility(false);

			RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.AllStolen>()
				.Subscribe(_ =>
				{
					SetGameOverVisibility(true);
					Observable.Timer(TimeSpan.FromSeconds(4))
						.Subscribe(_ => SceneManager.LoadScene("MainMenu"))
						.AddTo(this);

				})
				.AddTo(this);
		}

		private void SetGameOverVisibility(bool visible)
			=> _text.enabled = visible;
	}
}
