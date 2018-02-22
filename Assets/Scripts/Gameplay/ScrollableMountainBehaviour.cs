using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;
using System.Security.AccessControl;
using UnityEngine.SceneManagement;


public class ScrollableMountainBehaviour : MonoBehaviour
{
	private static ScrollableMountainBehaviour _instance;
	private bool _canContinue;

	public static Transform activeChild
	{
		get;
		private set;
	}

	private GameSettings settings;
	private InputBehaviour _input;
	private Transform[] _children;

	private float _initialMoveSpeed;
	private float _distance;

	private float _lowerBound;
	private float _moveSpeedDump;
	private float _moveSpeed;

	private int _currentHeight = 0;

	public static float currentMoveSpeed
	{
		get
		{
			return _instance._moveSpeed;
		}
	}

	public static void Launch()
	{
		_instance._canContinue = true;
	}

	private void Awake()
	{
		_instance = this;
		settings = GameSettings.instance;

		FindObjectOfType<InputBehaviour> ().beganHold += () => enabled = false;
		FindObjectOfType<InputBehaviour> ().endHold += () => enabled = true;

		_children = new Transform[transform.childCount];

		_moveSpeedDump = settings.mountainSpeedDump;
		_moveSpeed = settings.mountainSpeed;
		_initialMoveSpeed = _moveSpeed;

		for (int i = 0; i < transform.childCount; i++)
		{
			_children [i] = transform.GetChild (i);
		} 

		_distance = Vector2.Distance (_children [0].position, _children [1].position);
		_lowerBound = transform.position.y - _distance;
	}

	private void Update()
	{
		if (_canContinue == false)
		{
			return;
		}
		_moveSpeed = Mathf.MoveTowards (_moveSpeed, _initialMoveSpeed, _moveSpeedDump * Time.deltaTime);

		for (int i = 0; i < transform.childCount; i++)
		{
			var position = (Vector2)_children [i].localPosition;
			position = Vector2.MoveTowards (position, position + Vector2.down, _moveSpeed * Time.deltaTime);

			var condition = position.y < _lowerBound;
			if (condition)
			{
				position.y += _distance * 2f;
				activeChild = _children [i];
				_currentHeight += settings.mountainPeriod;
				var prev = PlayerPrefs.GetInt ("max");
				if (_currentHeight > prev)
				{
					PlayerPrefs.SetInt ("max", _currentHeight - 20);
				}

				var flagText = _children [i].GetComponentInChildren <Text> ();
				if (flagText != null)
				{
					flagText.text = _currentHeight.ToString ();
				}
			}

			_children [i].localPosition = position;
		}
	}
}
