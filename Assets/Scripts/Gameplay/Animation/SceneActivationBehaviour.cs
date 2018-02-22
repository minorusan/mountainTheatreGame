using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SceneActivationBehaviour : MonoBehaviour
{
	private Action _current;

	public BackgroundAppearenceBehaviour background;
	public float delayForBackground;

	public CurtainsAppearenceBehaviour curtains;
	public float delayForCurtains;

	public Animator playerStartAnimator;
	public PlayerAppearenceBehaviour playerAppearence;
	public float delayForPlayerStart;

	// Use this for initialization
	private void Awake()
	{
		playerStartAnimator.enabled = true;
		curtains.finishedMovement += () => {
			StartAnimationDelayed (background.PerformAnimation, delayForBackground);

		};

		background.finishedMovement += () => {
			StartAnimationDelayed (() => {
					
				}, delayForPlayerStart);
		};

		playerAppearence.finishedMovement += () => {
			ScrollableMountainBehaviour.Launch ();
		};
	}

	private void Start()
	{
		StartAnimationDelayed (curtains.PerformAnimation, delayForCurtains);
	}

	private void StartAnimationDelayed(Action method, float delay)
	{
		_current = method;
		Invoke ("DelayedInvoke", delay);
	}

	private void DelayedInvoke()
	{
		_current ();
	}
}
