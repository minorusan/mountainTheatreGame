using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class BackgroundAppearenceBehaviour : MonoBehaviour
{
	public event Action finishedMovement;

	public float appearSpeed;
	public float delay;
	public float offsetX;
	public Ease ease;

	private void OnEnable()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			var child = transform.GetChild (i);
			var childPosition = child.localPosition;
			var cachedX = childPosition.x;

			childPosition.x += cachedX > 0 ? offsetX : -offsetX;
			child.localPosition = childPosition;

			child.DOLocalMoveX (cachedX, appearSpeed + (i * delay)).SetEase (ease);
		}	
	}
}
