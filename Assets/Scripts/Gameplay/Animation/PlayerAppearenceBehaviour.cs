using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerAppearenceBehaviour : MonoBehaviour
{
	private RopeBehaviour _rope;
	private InputBehaviour _input;
	private Rigidbody2D _body;

	public float animationYOffset = 1.69f;

	public event Action finishedMovement;

	private void Awake()
	{
		_input = FindObjectOfType<InputBehaviour> ();
		_input.enabled = false;

		_body = GetComponentInParent <Rigidbody2D> ();
		_body.isKinematic = true;

		_rope = GetComponentInChildren <RopeBehaviour> ();
		_rope.enabled = false;
	}


	public void StartAnimationFinished()
	{
		_rope.enabled = true;
		_body.isKinematic = false;
		_input.enabled = true;

		finishedMovement ();
		var parentPosition = transform.parent.position;
		parentPosition.y += animationYOffset;
		transform.parent.position = parentPosition;
	}
}
