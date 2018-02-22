using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class BackgroundAppearenceBehaviour : MonoBehaviour
{
	public event Action finishedMovement;

	private Dictionary<Transform, float> _transformToX = new Dictionary<Transform, float> ();

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
			_transformToX.Add (child, cachedX);

		}	
	}

	public void PerformAnimation()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			var child = transform.GetChild (i);
			child.DOLocalMoveX (_transformToX [child], appearSpeed + (i * delay)).SetEase (ease);
		}

		Invoke ("Finished", (transform.childCount * delay) + appearSpeed);
	}

	private void Finished()
	{
		finishedMovement ();
	}
}
