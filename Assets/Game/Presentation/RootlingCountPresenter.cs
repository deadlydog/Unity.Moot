using Assets.Game.Domain;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Assets.Game.Presentation
{
	public class RootlingCountPresenter : MonoBehaviour
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
			var registeredCounts = RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.Registered>()
				.Select(registered => registered.RootlingsCount);

			var stolenCounts = RootlingsEvents
				.OfType<RootlingsEvent, RootlingsEvent.Stolen>()
				.Select(stolen => stolen.RemainingCount);

			registeredCounts
				.Merge(stolenCounts)
				.Subscribe(UpdateCount)
				.AddTo(this);
		}

		private void UpdateCount(int rootlingsCount)
		{
			_text.text = rootlingsCount.ToString();
		}
	}
}
