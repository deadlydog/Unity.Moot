using UnityEngine;

namespace Assets.Game.Presentation
{
	public class BranchPresenter : MonoBehaviour
	{
		private Rigidbody2D _rigidbody2d;
		private Vector3 _connectionPoint;
		private Quaternion _origRotation;

		void Awake()
		{
			_rigidbody2d = GetComponent<Rigidbody2D>();

			_connectionPoint = transform.localPosition;
			_origRotation = transform.localRotation;
		}

		void Update()
		{
			if (transform.parent != null)
			{
				transform.localPosition = _connectionPoint;
				transform.localRotation = _origRotation;
			}
		}
	}
}