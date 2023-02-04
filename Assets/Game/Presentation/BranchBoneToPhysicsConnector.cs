using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchBoneToPhysicsConnector : MonoBehaviour
{
	public GameObject SpriteBone;

	private void Start()
	{
		if (SpriteBone != null)
		{
			transform.position = SpriteBone.transform.position;
			transform.rotation = SpriteBone.transform.rotation;
		}
	}

	void Update()
	{
		if (SpriteBone != null)
		{
			SpriteBone.transform.position = transform.position;
			SpriteBone.transform.rotation = transform.rotation;
		}
	}
}
