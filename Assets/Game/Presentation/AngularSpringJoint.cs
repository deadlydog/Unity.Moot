using UnityEngine;

namespace Assets.Game.Presentation
{
	public class AngularSpringJoint : MonoBehaviour
	{
		public float TorqueCoefficient = 1;

		private Rigidbody2D _rigidbody2d;
		private float _originalRotation;
		private Rigidbody2D _connectedRigidbody2d;

		void Awake()
		{
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_connectedRigidbody2d = GetComponent<HingeJoint2D>().connectedBody;
			_originalRotation = _rigidbody2d.rotation;
		}
		
		void FixedUpdate()
		{
			var targetRotation = _connectedRigidbody2d != null
				? _connectedRigidbody2d.rotation + _originalRotation
				: _originalRotation;

			var rotationDiff = _rigidbody2d.rotation - targetRotation;

			_rigidbody2d.AddTorque(TorqueCoefficient * -rotationDiff);
		}
	}
}