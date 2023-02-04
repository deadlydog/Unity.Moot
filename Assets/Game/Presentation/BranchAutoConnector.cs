using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchAutoConnector : MonoBehaviour
{
	public GameObject SpriteBranchTip;

    void Start()
    {
		var physicsGameObject = gameObject;

		var spriteGameObject = SpriteBranchTip;

		while (physicsGameObject != null && spriteGameObject != null)
		{
			physicsGameObject.AddComponent<BranchBoneToPhysicsConnector>().SpriteBone = spriteGameObject;

			physicsGameObject = physicsGameObject.TryGetComponent<HingeJoint2D>(out var hinge) && hinge.connectedBody is Rigidbody2D rigidBody ? rigidBody.gameObject : null;
			spriteGameObject = spriteGameObject.transform.parent is Transform transform ? transform.gameObject : null;
		}
    }
}
