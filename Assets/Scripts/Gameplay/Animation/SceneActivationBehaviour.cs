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

	// Use this for initialization
	private void Awake()
	{
		curtains.finishedMovement += () => {
			StartAnimationDelayed (background.PerformAnimation, delayForBackground);
		};

		background.finishedMovement += () => {
			
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
