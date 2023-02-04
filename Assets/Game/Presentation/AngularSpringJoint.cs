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

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		void FixedUpdate()
		{
			var rotationDiff = _rigidbody2d.rotation - _originalRotation;

			Debug.Log(rotationDiff);

			_rigidbody2d.AddTorque(-rotationDiff);
		}
	}
}