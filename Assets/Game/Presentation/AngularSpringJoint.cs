using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Presentation
{
	public class AngularSpringJoint : MonoBehaviour
	{
		private Rigidbody2D _rigidbody2d;
		private float _originalRotation;

		void Awake()
		{
			_rigidbody2d = GetComponent<Rigidbody2D>();
			_originalRotation = _rigidbody2d.rotation;
		}
		
		void FixedUpdate()
		{
			var rotationDiff = _rigidbody2d.rotation - _originalRotation;

			_rigidbody2d.AddTorque(-rotationDiff);
		}
	}
}