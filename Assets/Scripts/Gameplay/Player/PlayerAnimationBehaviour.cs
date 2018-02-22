using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimationBehaviour : MonoBehaviour
{
	private PlayerAudioBehaviour _audioBehaviour;
	private Animator _animator;
	private bool _reachedSurfaceBlock;

	public GameObject particles;
	public Transform rightLeg;

	private void Start()
	{
		_audioBehaviour = FindObjectOfType<PlayerAudioBehaviour> ();
		var inputHandler = FindObjectOfType<InputBehaviour> ();
		_animator = GetComponent <Animator> ();

		inputHandler.beganHold += () => {
			_animator.SetBool ("hold", true);
		};

		inputHandler.endHold += () => {
			_animator.SetBool ("hold", false);
		};

		inputHandler.releasedWhileInAir += () => {
			_animator.SetBool ("losingVelocity", false);
			Invoke ("ReturnVelocity", 0.3f);
			_animator.SetTrigger ("midAirtJump");
		};

		inputHandler.willReachSurface += () => {
			if (!_reachedSurfaceBlock)
			{
				_animator.SetBool ("surface", true);
				_reachedSurfaceBlock = true;
				Invoke ("NoBlock", 0.5f);
			}

		};

		inputHandler.jumpedOffSurface += () => {
			_animator.SetBool ("surface", false);
		};
	}

	public void OnLegSweep()
	{
		var obj = Instantiate (particles);

		obj.SetActive (true);

		_audioBehaviour.PlaySound (EAudioEventType.rocksFalling);
		obj.transform.position = rightLeg.position;
	}

	private void NoBlock()
	{
		_reachedSurfaceBlock = false;
	}

	private void ReturnVelocity()
	{
		_animator.SetBool ("losingVelocity", true);
	}
}
