using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class CurtainsAppearenceBehaviour : MonoBehaviour
{
	public event Action finishedMovement;

	public float openTime;
	[Range (0f, 1f)]
	public float xPositionOffset = 1f;
	public Ease ease;

	public void PerformAnimation()
	{
		var upperCurtains = transform.GetChild (0);
		for (int i = 0; i < upperCurtains.childCount; i++)
		{
			var child = upperCurtains.GetChild (i);
			child.DOLocalMoveX ((child.localPosition.x * 3f) * xPositionOffset, openTime).SetEase (ease);
		}

		Invoke ("Finished", openTime);
	}

	private void Finished()
	{
		finishedMovement ();
	}
}
