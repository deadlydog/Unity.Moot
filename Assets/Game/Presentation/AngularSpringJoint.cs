using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Game.Presentation
{
	public class AngularSpringJoint : MonoBehaviour
	{
		public float TorqueCoefficient = 1;

		public float DifferentialCoeff = 1f;
		public float IntegralCoeff = 0.1f;
		public List<AngularSpringJoint> ChildBranches;

		private Rigidbody2D _rigidbody2d;
		private float _originalRotation;
		private Rigidbody2D _connectedRigidbody2d;
		private float _originalRotationDiff;

		private float _integral;

		void Awake()
		{
			ChildBranches = new List<AngularSpringJoint>();
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_connectedRigidbody2d = GetComponent<HingeJoint2D>().connectedBody;
			_originalRotation = _rigidbody2d.rotation;
			_originalRotationDiff = _originalRotation - _connectedRigidbody2d.rotation;
		}

		private void Start()
		{
			if (transform.parent != null)
			{
				if (transform.parent.TryGetComponent<AngularSpringJoint>(out var joint))
					joint.ChildBranches.Add(this);
				else if (transform.parent.TryGetComponent<BranchPresenter>(out var presenter)
					&& presenter.transform.parent != null
					&& presenter.transform.parent.TryGetComponent(out joint))
					joint.ChildBranches.Add(this);
			}

		}

		void FixedUpdate()
		{
			var targetRotation = _connectedRigidbody2d != null
				? _connectedRigidbody2d.rotation + _originalRotationDiff
				: _originalRotation;

			var rotationDiff = targetRotation - _rigidbody2d.rotation;

			rotationDiff %= 360;

			if (rotationDiff < -180)
				rotationDiff += 360;
			if (rotationDiff > 180)
				rotationDiff -= 360;

			_integral += rotationDiff;

			var torque = TorqueCoefficient * -rotationDiff
				+ IntegralCoeff * _integral
				+ DifferentialCoeff * _rigidbody2d.angularVelocity;
			
			_rigidbody2d.AddTorque(torque);
			
			var targetVelocity = rotationDiff / Time.fixedDeltaTime;

			var velocityDiff = targetVelocity - _rigidbody2d.angularVelocity;
			var velocityDiffInRadians = velocityDiff * Mathf.Deg2Rad;

			var torqueToReset = velocityDiffInRadians * 50 * _rigidbody2d.mass;
			var targetTorque = TorqueCoefficient * rotationDiff;

			var finalTorque = Mathf.Lerp(torqueToReset, targetTorque, 0.8f);

			ApplyTorque(targetTorque, transform.position);
		}

		private void ApplyTorque(float targetTorque, Vector2 origin)
		{
			var offset = (Vector2)transform.position - origin;
			var newOffset = RotateVector(offset, Mathf.Sign(targetTorque));
			var end = origin + newOffset;
			var forceDirection = end - (Vector2)transform.position;

			_rigidbody2d.AddTorque(targetTorque * _rigidbody2d.mass);
			_rigidbody2d.AddForce(forceDirection * Mathf.Abs(targetTorque) * _rigidbody2d.mass);

			foreach (var child in ChildBranches)
				child.ApplyTorque(targetTorque * 0.5f, origin);
		}

		public Vector2 RotateVector(Vector2 vector, float degrees) 
			=> Quaternion.Euler(0, 0, degrees) * vector;
	}
}