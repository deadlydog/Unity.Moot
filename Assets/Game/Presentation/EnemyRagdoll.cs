using Assets.Game.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.U2D.IK;
using Zenject;

public class EnemyRagdoll : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private List<Collider2D> colliders;
	[SerializeField] private List<HingeJoint2D> joints;
	[SerializeField] private List<Rigidbody2D> rigidBodies;
	[SerializeField] private List<LimbSolver2D> limbSolvers;
	[SerializeField] private Collider2D nonRagdollCollider;
	
	private Rigidbody2D _rigidbody2d;

	[Inject]
	public EnemyParameters Parameters { private get; set; }

	void Awake()
	{
		_rigidbody2d = GetComponent<Rigidbody2D>();
	}

	private void Start()
	{
		RagdollOff();

		foreach (var body in rigidBodies)
			body.mass = Parameters.EnemyRagdollLimbMass;
	}

	public void RagdollOn() => ToggleRagdoll(true);
	public void RagdollOff() => ToggleRagdoll(false);
	private void ToggleRagdoll(bool ragdollOn)
	{
		animator.enabled = !ragdollOn;
		nonRagdollCollider.enabled = !ragdollOn;

		foreach (var col in colliders)
			col.enabled = ragdollOn;

		foreach (var joint in joints)
			joint.enabled = ragdollOn;

		foreach (var body in rigidBodies)
		{
			body.simulated = ragdollOn;
			body.velocity = _rigidbody2d.velocity;
			body.angularVelocity = _rigidbody2d.angularVelocity;
		}

		foreach (var limbSolver in limbSolvers)
		{
			// Can use this instead to have more control
			limbSolver.weight = ragdollOn ? 0 : 1;
			// limbSolver.enabled = !ragdollOn;
		}
	}
}
