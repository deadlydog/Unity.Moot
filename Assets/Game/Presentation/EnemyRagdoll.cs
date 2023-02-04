using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.U2D.IK;

public class EnemyRagdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private List<Collider2D> colliders;
    [SerializeField] private List<HingeJoint2D> joints;
    [SerializeField] private List<Rigidbody2D> rigidBodies;
    [SerializeField] private List<LimbSolver2D> limbSolvers;
    [SerializeField] private Collider2D nonRagdollCollider;

    private void Start()
    {
        RagdollOff();
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
            body.simulated = ragdollOn;

        foreach (var limbSolver in limbSolvers)
        {
            // Can use this instead to have more control
            limbSolver.weight = ragdollOn ? 0 : 1;
            // limbSolver.enabled = !ragdollOn;
        }
    }
}
