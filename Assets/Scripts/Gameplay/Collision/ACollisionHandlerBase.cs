using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ACollisionHandlerBase : MonoBehaviour
{
	public event Action<string> collidedWithTag;
	public event Action<Transform> collidedWithTransform;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collidedWithTag != null)
		{
			collidedWithTag (collision.collider.tag);
		}
		if (collidedWithTransform != null)
		{
			collidedWithTransform (collision.transform);
		}
	}
}
