using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using DG.Tweening;


public class InputBehaviour : MonoBehaviour
{
	public enum EState
	{
		Air,
		Hold,
		Climbing
	}

	private bool _isPressed;
	private float _tapInputTime;

	private Rigidbody2D _body;
	private Transform _player;

	#region Events

	public event Action tappedWhileInAir;
	public event Action releasedWhileInAir;

	public event Action beganHold;
	public event Action endHold;

	public event Action reachedSurface;
	public event Action willReachSurface;
	public event Action<Transform> reachedSurfaceWithParam;

	public event Action jumpedOffSurface;

	#endregion


	public EState state = EState.Air;
	public PlayerInputSettings settings;

	private void Start()
	{
		var player = GameObject.FindGameObjectWithTag ("Player");

		player.GetComponentInChildren<MountainCollisionHandler> ().collidedWithTag += (string obj) => {

			if (state != EState.Climbing)
			{
				_body.velocity = Vector2.zero;
				state = EState.Climbing;
				if (reachedSurface != null)
				{
					reachedSurface ();
				}
			}
		};

		_body = player.GetComponent <Rigidbody2D> ();
		_player = player.transform;
	}

	private void Update()
	{
		if (_isPressed)
		{
			_tapInputTime += Time.deltaTime;
		}

		HandleState ();
	}

	private void OnMouseDown()
	{
		_isPressed = true;
		if (state == EState.Climbing)
		{
			state = EState.Hold;
			beganHold ();
		}
	}

	private void OnMouseUp()
	{
		if (_tapInputTime < settings.tapTimeTrashhold && state == EState.Hold)
		{
			state = EState.Air;

			if (jumpedOffSurface != null)
			{
				jumpedOffSurface ();
			}

			_player.DOJump (_player.position + (Vector3.right * settings.initialJumpLenght), settings.jumpPower, 1, 
				settings.jumpTime);
		}
		else if (state == EState.Hold)
		{
			state = EState.Climbing;

		}
		else if (state == EState.Air)
		{
			if (releasedWhileInAir != null)
			{
				releasedWhileInAir ();
			}
		}

		endHold ();
		_tapInputTime = 0f;
		_isPressed = false;
	}

	private void HandleState()
	{
		switch (state)
		{
			case EState.Air:
				{
					var position = (Vector2)_player.position;
					if (Input.GetMouseButtonDown (0) && tappedWhileInAir != null)
					{
						tappedWhileInAir ();
					}

					var mouseDown = Input.GetMouseButtonDown (0);
					if (mouseDown)
					{
						_body.velocity = Vector2.zero;
						_body.isKinematic = true;
						if (position.x < settings.maxX)
						{
							_player.DOJump (_player.position + (Vector3.right * settings.initialJumpLenght), settings.jumpPower, 1, 
								settings.jumpTime);
						}
					}
					else
					{
						if (position.x < settings.reachingSurface)
						{
							if (willReachSurface != null)
							{
								willReachSurface ();
							}
						}
						_body.isKinematic = false;
					}

					break;
				}
			case EState.Hold:
				{
					break;
				}
			default:
				break;
		}
	}
}
