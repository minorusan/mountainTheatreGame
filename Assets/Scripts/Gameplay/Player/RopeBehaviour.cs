using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RopeBehaviour : MonoBehaviour
{
	private LineRenderer _renderer;

	public bool debug;
	public Transform lineEnd;
	public Transform lineStart;
	public Transform leftArm;
	public Transform rightArm;

	void Start()
	{
		_renderer = GetComponent <LineRenderer> ();
	}

	void Update()
	{
		if (_renderer != null)
		{
			lineStart.transform.position = new Vector3 (leftArm.transform.position.x,
				lineStart.transform.position.y, lineEnd.transform.position.z);
			
			_renderer.SetPosition (0, lineEnd.position);

			_renderer.SetPosition (1, rightArm.position);
			_renderer.SetPosition (2, leftArm.position);
			_renderer.SetPosition (3, lineStart.position);
		}
		else
		{
			Destroy (this);
		}
	}

	private void OnDrawGizmos()
	{
		if (debug)
		{
			_renderer = GetComponent <LineRenderer> ();
			Update ();
		}
	}
}
