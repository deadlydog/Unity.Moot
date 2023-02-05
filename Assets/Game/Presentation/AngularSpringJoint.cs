using UnityEngine;

namespace Assets.Game.Presentation
{
	public class AngularSpringJoint : MonoBehaviour
	{
		public float TorqueCoefficient = 1;

		private Rigidbody2D _rigidbody2d;
		private float _originalRotation;
		private Rigidbody2D _connectedRigidbody2d;
		private float _originalRotationDiff;

		void Awake()
		{
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_connectedRigidbody2d = GetComponent<HingeJoint2D>().connectedBody;
			_originalRotation = _rigidbody2d.rotation;
			_originalRotationDiff = _originalRotation - _connectedRigidbody2d.rotation;
		}
		
		void FixedUpdate()
		{
			var targetRotation = _connectedRigidbody2d != null
				? _connectedRigidbody2d.rotation + _originalRotationDiff
				: _originalRotation;

			var rotationDiff = _rigidbody2d.rotation - targetRotation;

			rotationDiff %= 360;

			if (rotationDiff < -180)
				rotationDiff += 360;
			if (rotationDiff > 180)
				rotationDiff -= 360;
			
			_rigidbody2d.AddTorque(TorqueCoefficient * -rotationDiff);
		}
	}
}